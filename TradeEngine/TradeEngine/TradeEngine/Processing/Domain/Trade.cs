namespace TradeEngine.Processing.Domain;

/// <summary>
/// A trade is the result of a trade request.
///
/// The properties of a trade are immutable.
/// </summary>
public record Trade(int Id, int MarketId, decimal Quantity, decimal BookingPrice);