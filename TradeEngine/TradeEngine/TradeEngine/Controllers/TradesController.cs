using Microsoft.AspNetCore.Mvc;
using TradeEngine.DataContracts;

namespace TradeEngine.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TradesController : Controller
{
    private readonly Processing.TradeEngine _tradeEngine;

    public TradesController(Processing.TradeEngine tradeEngine)
    {
        _tradeEngine = tradeEngine;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateTradeRequest tradeRequest)
    {
        var trade = _tradeEngine.BookTrade(tradeRequest.MarketId, tradeRequest.Quantity, tradeRequest.BookingPrice);
        return Ok(trade.Id);
    }
}