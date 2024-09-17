namespace TradeEngine.DataContracts;

public class CreateTradeRequest
{
    public int MarketId { get; set; }
    public decimal Quantity { get; set; }
    public decimal BookingPrice { get; set; }
}