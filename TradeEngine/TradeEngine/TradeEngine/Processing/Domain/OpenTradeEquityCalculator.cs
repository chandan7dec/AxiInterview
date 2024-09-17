namespace TradeEngine.Processing.Domain;

public static class OpenTradeEquityCalculator
{
    public static decimal Calculate(Trade trade, Market market, Price price)
    {
        var priceDifference = price.Value - trade.BookingPrice;
        var normalisedTradeQuantity = trade.Quantity * market.QuantityConversionFactor;

        return priceDifference * normalisedTradeQuantity;
    }
}