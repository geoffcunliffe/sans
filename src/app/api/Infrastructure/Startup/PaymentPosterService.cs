using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sans.CreditUnion.Database.Constants;
using Sans.CreditUnion.Database.Context;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sans.CreditUnion.API.Infrastructure.Startup
{
    public class PaymentPosterService : BackgroundService
    {
        private readonly ILogger<PaymentPosterService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _services;

        public PaymentPosterService(ILogger<PaymentPosterService> logger, IConfiguration configuration, IServiceProvider services)
        {
            _logger = logger;
            _configuration = configuration;
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string serviceName = nameof(PaymentPosterService);

            _logger.LogDebug($"{serviceName} is starting.");

            stoppingToken.Register(() => _logger.LogDebug($"{serviceName} background task is stopping."));

            int restartTimeInMilliseconds = Convert.ToInt32(_configuration["PaymentPosterTimeoutInMilliseconds"]);

            while (!stoppingToken.IsCancellationRequested)
            {

                using (var scope = _services.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<SansCreditUnionDbContext>();

                    _logger.LogDebug($"{serviceName} task started posting transactions.");

                    PostPendingTransactions(dbContext);

                    _logger.LogDebug($"{serviceName} task started posting trades.");

                    PostPendingTrades(dbContext);
                }

                _logger.LogDebug($"{serviceName} task finished.  Waiting {restartTimeInMilliseconds} milliseconds.");


                await Task.Delay(restartTimeInMilliseconds, stoppingToken);
            }

            _logger.LogDebug($"{serviceName} background task is stopping.");
        }

        private void PostPendingTransactions(SansCreditUnionDbContext dbContext)
        {
            var pendingPayments = dbContext.Transactions
                .Where(t => t.PostedDateTime == null)
                .Include(t => t.Account)
                .ToList();

            foreach (var payment in pendingPayments)
            {
                payment.Account.Balance += payment.Amount;
                payment.PostedDateTime = DateTime.UtcNow;
                dbContext.SaveChanges();
            }
        }

        private void PostPendingTrades(SansCreditUnionDbContext dbContext)
        {
            var pendingTrade = dbContext.BrokerageTrades
                .Where(t => t.PostedDateTime == null)
                .Include(t => t.BrokerageAccount)
                .ToList();

            foreach (var trade in pendingTrade)
            {
                var account = dbContext.Accounts
                    .Where(e => e.Id == trade.AccountId)
                    .Single();

                if (trade.BrokerageTradeType == BrokerageTradeTypes.Buy)
                {
                    account.Balance -= trade.Price * trade.Quantity;
                }
                else // Must be a sell
                {
                    account.Balance += trade.Price * trade.Quantity;
                }

                trade.PostedDateTime = DateTime.UtcNow;

                dbContext.SaveChanges();
            }
        }
    }
}
