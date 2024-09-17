namespace TradeEngine.Processing.Domain;

public class TradeValuation
{
    public Trade Trade { get; }
    public decimal OpenTradeEquity { get; }
    public Price Price { get; }

    public TradeValuation(Trade trade, decimal openTradeEquity, Price price)
    {
        Trade = trade;
        OpenTradeEquity = openTradeEquity;
        Price = price;
    }
}