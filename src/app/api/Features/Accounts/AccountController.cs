using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sans.CreditUnion.API.Services.Interfaces;
using Sans.CreditUnion.Database.Models;

namespace Sans.CreditUnion.API.Features.Accounts
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IHttpContextAccessor httpContextAccessor, IAccountService accountService)
        {
            _httpContextAccessor = httpContextAccessor;
            _accountService = accountService;
        }

        [HttpGet]
        public ActionResult<List<Account>> Get()
        {
            string userId = _httpContextAccessor.HttpContext.User.GetUserId();
            return _accountService.GetAllAccountsByUserId(userId);
        }
    }
}