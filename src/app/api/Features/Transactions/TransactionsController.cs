using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sans.CreditUnion.API.Features.Transactions.Models;
using Sans.CreditUnion.API.Services.Interfaces;
using Sans.CreditUnion.Database.Models;

namespace Sans.CreditUnion.API.Features.Transactions
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionsService _transactionService;

        public TransactionsController(IHttpContextAccessor httpContextAccessor, ITransactionsService transactionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _transactionService = transactionService;
        }

        [HttpGet]
        public ActionResult<List<Transaction>> Get()
        {
            long memberId = _httpContextAccessor.HttpContext.User.GetMemberId();
            return _transactionService.GetAllTransactionsByMemberId(memberId);
        }

        [HttpPost(nameof(TransferFunds))]
        public IActionResult TransferFunds(TransferFundsRequest model)
        {
            model.SenderUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var result = _transactionService.TransferFunds(model);

            if (result == TransferFundsResult.ReceiverEmailNotFound)
                return NotFound($"No user with email {model.ReceiverEmail} exists.");

            return Ok();
        }
    }
}