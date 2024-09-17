using NUnit.Framework;
using TradeEngine.Processing.Domain;

namespace TradeEngine.UnitTests.Processing;

[TestFixture]
public class TradeEngineTests
{
    [Test]
    public void WhenAMarketIsNotPresentForATrade_ThenAKeyNotFoundExceptionIsThrown()
    {
        var tradeEngine = new TradeEngine.Processing.TradeEngine();
        Assert.Throws<KeyNotFoundException>(() => tradeEngine.AddOrUpdateTrade(new Trade(1, 2, 10, 10.1m)));
    }

    [Test]
    public void WhenAMarketAndPriceArePresentForATrade_ThenATradeUpdateEventIsSentOut()
    {
        var marketId = 2;

        var tradeValuationsUpdated = new List<TradeValuation>();
        var tradeEngine = new TradeEngine.Processing.TradeEngine();
        tradeEngine.TradeValuationUpdated += (_, a) => tradeValuationsUpdated.Add(a.Valuation);
        tradeEngine.AddOrUpdateMarket(new Market(marketId, 1));
        tradeEngine.UpdatePrice(new MarketPrice(marketId, new Price(1, 15m)));
        tradeEngine.AddOrUpdateTrade(new Trade(2, marketId, 10, 10.1m));

        Assert.That(tradeValuationsUpdated.Count, Is.EqualTo(1));
        Assert.That(tradeValuationsUpdated.Single().Trade.Id, Is.EqualTo(2));
    }
}