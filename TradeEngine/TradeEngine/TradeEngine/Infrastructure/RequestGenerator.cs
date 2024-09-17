using TradeEngine.Processing.Domain;

namespace TradeEngine.Infrastructure;

public class RequestGenerator
{
    private const int StartingQuantity = 10;
    private const int UpdatedQuantity = 15;
    private const decimal StartingQuantityConversionFactor = 1;
    private const decimal UpdatedQuantityConversionFactor = 0.1m;
    private const int BookingPrice = 100;

    private readonly Processing.TradeEngine _tradeEngine;
    private readonly int _countPriceUpdatesPerMarket;
    private readonly int _countMarkets;
    private readonly int _countTradesPerMarket;
    private readonly Task _priceGenerator;
    private readonly Task _tradeUpdateGenerator;
    private readonly Task _marketUpdateGenerator;

    private int _price = 100;
    private int _priceId = 0;

    public RequestGenerator(Processing.TradeEngine tradeEngine, int countPriceUpdatesPerMarket, int countMarkets, int countTradesPerMarket)
    {
        _tradeEngine = tradeEngine;
        _countPriceUpdatesPerMarket = countPriceUpdatesPerMarket;
        _countMarkets = countMarkets;
        _countTradesPerMarket = countTradesPerMarket;

        _priceGenerator = new Task(() => UpdatePrices(tradeEngine));
        _tradeUpdateGenerator = new Task(() => AddOrUpdateTrades(tradeEngine, UpdatedQuantity));
        _marketUpdateGenerator = new Task(() => AddOrUpdateMarkets(tradeEngine, UpdatedQuantityConversionFactor));
    }

    private void AddOrUpdateMarkets(Processing.TradeEngine tradeEngine, decimal quantityConversionFactor)
    {
        foreach (var i in Enumerable.Range(0, _countMarkets))
        {
            tradeEngine.AddOrUpdateMarket(new Market(i, quantityConversionFactor));
        }
    }

    private void SetInitialPrices(Processing.TradeEngine tradeEngine)
    {
        foreach (var id in Enumerable.Range(0, _countMarkets))
        {
            tradeEngine.UpdatePrice(new MarketPrice(id, new Price(_price, ++_priceId)));
        }
    }

    private void AddOrUpdateTrades(Processing.TradeEngine tradeEngine, decimal quantity)
    {
        int countTotalTrades = _countTradesPerMarket * _countMarkets;
        foreach (var i in Enumerable.Range(0, countTotalTrades))
        {
            var marketId = i % _countMarkets;
            tradeEngine.AddOrUpdateTrade(new Trade(i, marketId, quantity, BookingPrice));
        }
    }

    private void UpdatePrices(Processing.TradeEngine tradeEngine)
    {
        var countTotalPriceUpdates = _countMarkets * _countPriceUpdatesPerMarket;
        foreach (var i in Enumerable.Range(0, countTotalPriceUpdates))
        {
            var marketId = i % _countMarkets;
            tradeEngine.UpdatePrice(new MarketPrice(marketId, new Price(++_price, ++_priceId)));

            // concurrently send some other updates part way through the price updates
            if (i == 5)
            {
                _marketUpdateGenerator.Start();
                _tradeUpdateGenerator.Start();
            }
        }
    }

    public Task[] Start()
    {
        AddOrUpdateMarkets(_tradeEngine, StartingQuantityConversionFactor);
        SetInitialPrices(_tradeEngine);
        AddOrUpdateTrades(_tradeEngine, StartingQuantity);

        _priceGenerator.Start();

        return new Task[]{
            _priceGenerator,
            _marketUpdateGenerator,
            _tradeUpdateGenerator
        };
    }
}