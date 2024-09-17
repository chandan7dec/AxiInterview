using TradeEngine.Processing.Domain;

namespace TradeEngine.Processing;

public class TradeEngine
{
    private readonly IDictionary<int, Trade> _tradesById = new Dictionary<int, Trade>();
    private readonly IDictionary<int, Market> _marketsById = new Dictionary<int, Market>();
    private readonly IDictionary<int, Price> _currentPriceByMarketId = new Dictionary<int, Price>();
    private int _lastTradeId;

    public event EventHandler<TradeValuationEventArgs>? TradeValuationUpdated;

    public void AddOrUpdateTrade(Trade trade)
    {
        _lastTradeId = Math.Max(trade.Id, _lastTradeId);

        _tradesById[trade.Id] = trade;
        var price = _currentPriceByMarketId[trade.MarketId];
        var market = _marketsById[trade.MarketId];

        CalculateOpenTradeEquity(trade, market, price);
    }

    public void AddOrUpdateMarket(Market market)
    {
        _marketsById[market.Id] = market;

        if (_currentPriceByMarketId.TryGetValue(market.Id, out var price))
        {
            CalculateOpenTradeEquityForTrades(market, price);
        }
    }

    public void UpdatePrice(MarketPrice marketPrice)
    {
        _currentPriceByMarketId[marketPrice.MarketId] = marketPrice.Price;
        var market = _marketsById[marketPrice.MarketId];
        CalculateOpenTradeEquityForTrades(market, marketPrice.Price);
    }

    private void CalculateOpenTradeEquityForTrades(Market market, Price price)
    {
        foreach (var trade in _tradesById.Values.Where(x => x.MarketId == market.Id))
        {
            CalculateOpenTradeEquity(trade, market, price);
        }
    }

    private void CalculateOpenTradeEquity(Trade trade, Market market, Price price)
    {
        var openTradeEquity = OpenTradeEquityCalculator.Calculate(trade, market, price);
        var tradeValuation = new TradeValuation(trade, openTradeEquity, price);

        var tradeValuationEventArgs = new TradeValuationEventArgs(tradeValuation);
        TradeValuationUpdated?.Invoke(this, tradeValuationEventArgs);
    }

    public Trade BookTrade(int marketId, decimal requestQuantity, decimal bookingPrice)
    {
        var newTradeId = _lastTradeId++;
        var trade = new Trade(newTradeId, marketId, requestQuantity, bookingPrice);
            
        AddOrUpdateTrade(trade);

        return trade;
    }
}