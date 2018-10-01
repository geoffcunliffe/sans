using Microsoft.AspNetCore.Mvc;
using Sans.CreditUnion.API.Services.Interfaces;
using Sans.CreditUnion.Database.Models;
using System.Collections.Generic;

namespace Sans.CreditUnion.API.Features.BrokerageStocks
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrokerageStocksController : Controller
    {
        private readonly IBrokerageStocksService _brokerageStocksService;

        public BrokerageStocksController(IBrokerageStocksService brokerageStocksService)
        {
            _brokerageStocksService = brokerageStocksService;
        }

        [HttpGet]
        public ActionResult<List<BrokerageStock>> Get()
        {
            return _brokerageStocksService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<BrokerageStock> Get(long id)
        {
            var stock = _brokerageStocksService.GetById(id);

            if (stock == null)
                return NotFound();

            return stock;
        }
    }
}
