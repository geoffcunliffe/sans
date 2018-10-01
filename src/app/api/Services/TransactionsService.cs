using System.Collections.Generic;
using System.Linq;
using Sans.CreditUnion.API.Features.Transactions.Models;
using Sans.CreditUnion.API.Services.Interfaces;
using Sans.CreditUnion.Database.Context;
using Sans.CreditUnion.Database.Models;

namespace Sans.CreditUnion.API.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly SansCreditUnionDbContext _dbContext;

        public TransactionsService(SansCreditUnionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Possible TODO: Paging? Get by month?  Get by account?  Not sure what UI is going to look like
        public List<Transaction> GetAllTransactionsByMemberId(long memberId)
        {
            return _dbContext.Transactions
                .Where(t => t.Account.Member.Id == memberId)
                .ToList();
        }

        public TransferFundsResult TransferFunds(TransferFundsRequest model)
        {
            var receivingAccountId = _dbContext.Accounts
                .Where(a => a.Member.User.Email == model.ReceiverEmail)
                .Select(a => a.Id)
                .SingleOrDefault();

            if (receivingAccountId == 0)
                return TransferFundsResult.ReceiverEmailNotFound;

            var senderAccountId = _dbContext.Accounts
                .Where(a => a.Member.User.Id == model.SenderUserId)
                .Select(a => a.Id)
                .Single();

            var transaction = new Transaction
            {
                Amount = model.Amount,
                Description = model.Description,
                ReceivingAccountId = receivingAccountId,
                SendingAccountId = senderAccountId,
            };

            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();

            return TransferFundsResult.Successful;
        }
    }
}