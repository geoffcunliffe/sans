using Microsoft.AspNetCore.Identity;
using Sans.CreditUnion.Database.Constants;
using Sans.CreditUnion.Database.Context;
using Sans.CreditUnion.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sans.CreditUnion.API.Infrastructure.Startup
{
    public static class SeedData
    {
        public const string JohnSmithGuid = "2610DE74-CCB9-919-81E3-1818795D4D36";
        public const string JohnSmithEmail = "jsmith@gmail.com";
        public const string JohnSmithAccountGuid = "8364188D-3F49-4AA8-B10F-91B87B94869C";

        public const string EveryonesPassword = "Password123!";

        public static void Seed(this SansCreditUnionDbContext context, IServiceProvider serviceProvider)
        {
            CreateAccountTypesIfTheyDontExist(context);
            CreateMembersIfTheyDontExist(context, serviceProvider);
            CreateStocksIfTheyDontExist(context);
            AddRandomStockPriceHistory(context);
        }

        private static void AddRandomStockPriceHistory(SansCreditUnionDbContext context)
        {
            List<long> stockIdsInDb = context.BrokerageStocks
                .Select(a => a.Id)
                .ToList();

            List<BrokerageStock> stocks = BrokerageStocks.All;

            var random = new Random();
            foreach (var stock in stocks)
            {
                var within10Percent = Convert.ToInt32(stock.PreviousDayClose * 0.1m);
                var stockPriceHistory = new BrokerageStockPriceHistory
                {
                    Price = stock.PreviousDayClose * random.Next(within10Percent * -1, within10Percent),
                    StockId = stock.Id,
                    Timestamp = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, random.Next(8, 17), random.Next(0, 59), random.Next(0, 59))
                };

                context.BrokerageStockPriceHistory.Add(stockPriceHistory);
            }

            context.SaveChanges();
        }

        public static void CreateMembersIfTheyDontExist(this SansCreditUnionDbContext context, IServiceProvider serviceProvider)
        {
            var userManger = serviceProvider.GetService(typeof(UserManager<CreditUnionUser>)) as UserManager<CreditUnionUser>;

            var johnSmithMember = new Member
            {
                Accounts = new List<Account>
                {
                    new Account
                    {
                        AccountNumber = "12345678",
                        Guid = JohnSmithAccountGuid,
                        AccountTypeId = (int) AccountTypeIds.Checking,
                        Balance = 50_000,
                        InterestRate = 0.0005m,
                    }
                },
                City = "Des Moines",
                FirstName = "John",
                Guid = JohnSmithGuid,
                LastName = "Smith",
                State = "IA",
                Street = "123 1st St",
                User = new CreditUnionUser
                {
                    Email = JohnSmithEmail,
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    NormalizedEmail = JohnSmithEmail.ToUpper(),
                    NormalizedUserName = JohnSmithEmail.ToUpper(),
                    PhoneNumber = "55512345678",
                    PhoneNumberConfirmed = true,
                    UserName = JohnSmithEmail
                },
                ZipCode = "50009"
            };

            var staticMembers = new Member[]
            {
                johnSmithMember
            };

            Member[] membersInDb = context.Members.ToArray();

            foreach (var member in staticMembers)
            {
                if (!membersInDb.Any(m => m.Guid == member.Guid))
                {
                    // Save off user
                    CreditUnionUser user = member.User;

                    // Null it out on the Member so it doesn't get saved to the DB.  
                    // Going to let UserManager handle creating the user with the necessary stuff like SecurityStamps, PasswordHash, etc.
                    member.User = null;
                    context.Add(member);

                    context.SaveChanges();

                    user.MemberId = member.Id;
                    userManger.CreateAsync(user, EveryonesPassword).GetAwaiter().GetResult();
                }
            }
        }

        public static void CreateAccountTypesIfTheyDontExist(this SansCreditUnionDbContext context)
        {
            List<int> accountTypeIdsInDb = context.AccountTypes
                .Select(a => a.Id)
                .ToList();

            var accountTypeIds = (int[])Enum.GetValues(typeof(AccountTypeIds));

            foreach (var accountTypeId in accountTypeIds)
            {
                if (!accountTypeIdsInDb.Contains(accountTypeId))
                    context.Add(new AccountType
                    {
                        Id = accountTypeId,
                        Description = Enum.GetName(typeof(AccountTypeIds), accountTypeId)
                    });
            }

            context.SaveChanges();
        }

        public static void CreateStocksIfTheyDontExist(this SansCreditUnionDbContext context)
        {
            List<long> stockIdsInDb = context.BrokerageStocks
                .Select(a => a.Id)
                .ToList();

            List<BrokerageStock> stocks = BrokerageStocks.All;

            foreach (var stock in stocks)
            {
                if (!stockIdsInDb.Contains(stock.Id))
                    context.Add(stock);
            }

            context.SaveChanges();
        }
    }
}
