using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TradeEngine.Infrastructure;
using TradeEngine.Processing.Domain;

namespace TradeEngine.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RequestGeneratorController : Controller
{
    private const int CountTradesPerMarket = 10;
    private const int CountPriceUpdatesPerMarket = 1000;
    private const int CountMarkets = 100;

    private readonly ILogger<RequestGeneratorController> _logger;
    private readonly RequestGenerator _generator;
    private readonly IPublisher<TradeValuation> _publisher;
    private readonly Processing.TradeEngine _tradeEngine;

    public RequestGeneratorController(ILogger<RequestGeneratorController> logger,
        Processing.TradeEngine tradeEngine,
        IPublisher<TradeValuation> publisher)
    {
        _tradeEngine = tradeEngine;
        _generator = new RequestGenerator(tradeEngine, CountPriceUpdatesPerMarket, CountMarkets, CountTradesPerMarket);
        _logger = logger;
        _publisher = publisher;
    }

    [HttpPost("generate")]
    public async Task<IActionResult> Generate()
    {
        var updates = 0;
        _tradeEngine.TradeValuationUpdated += (_, x) =>
        {
            updates++;
            _publisher.Publish(x.Valuation);
        };

        _logger.LogInformation("Waiting for all updates to be processed");

        var stopwatch = Stopwatch.StartNew();
        await Task.WhenAll(_generator.Start());
        var elapsedMs = stopwatch.ElapsedMilliseconds;

        _logger.LogInformation("Consuming {UpdatesCount} price updates took {ElapsedMs}ms", updates, elapsedMs);

        return Ok($"Generated {updates} in {elapsedMs}ms");
    }
}