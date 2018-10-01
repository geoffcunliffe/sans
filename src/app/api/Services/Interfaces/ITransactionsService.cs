using System.Collections.Generic;
using Sans.CreditUnion.API.Features.Transactions.Models;
using Sans.CreditUnion.Database.Models;

namespace Sans.CreditUnion.API.Services.Interfaces
{
    public interface ITransactionsService
    {
        List<Transaction> GetAllTransactionsByMemberId(long memberId);
        TransferFundsResult TransferFunds(TransferFundsRequest model);
    }
}