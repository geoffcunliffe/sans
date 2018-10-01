using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sans.CreditUnion.API.Features.Home
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        [AllowAnonymous]
        public string Index()
        {
            return "Hello World";
        }
    }
}