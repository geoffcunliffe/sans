using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sans.CreditUnion.API.Features.BillPay.Models;
using Sans.CreditUnion.API.Services.Interfaces;
using System.Collections.Generic;

namespace Sans.CreditUnion.API.Features.BillPay
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillPayController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBillPayService _billPayService;

        public BillPayController(IHttpContextAccessor httpContextAccessor, IBillPayService billPayService)
        {
            _httpContextAccessor = httpContextAccessor;
            _billPayService = billPayService;
        }

        [HttpGet]
        public ActionResult<List<Database.Models.BillPay>> Get()
        {
            long memberId = _httpContextAccessor.HttpContext.User.GetMemberId();
            return _billPayService.GetAllByMemberId(memberId);
        }

        [HttpGet("{guid}")]
        public ActionResult<Database.Models.BillPay> Get(string guid)
        {
            long memberId = _httpContextAccessor.HttpContext.User.GetMemberId();
            var billPay = _billPayService.GetByGuidAndMemberId(guid, memberId);

            if (billPay == null)
                return NotFound();

            return billPay;
        }

        [HttpPost]
        public ActionResult<Database.Models.BillPay> Post(AddBillPayRequest model)
        {
            long memberId = _httpContextAccessor.HttpContext.User.GetMemberId();
            var result = _billPayService.AddBillPay(model, memberId);

            if (result.ResultType == AddBillPayResultType.PayeeNotFound)
                return NotFound($"Payee with guid {model.PayeeGuid} not found.");

            return CreatedAtAction(nameof(Get), new { guid = result.BillPay.Guid }, result.BillPay);
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(string guid)
        {
            long memberId = _httpContextAccessor.HttpContext.User.GetMemberId();
            var result = _billPayService.DeleteBillPay(guid, memberId);

            if (result == DeleteBillPayResult.NotFound)
                return NotFound();

            return Ok();
        }
    }
}
