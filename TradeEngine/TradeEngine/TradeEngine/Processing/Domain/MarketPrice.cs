namespace TradeEngine.Processing.Domain;

/// <summary>
/// The price for a specific market.
/// </summary>
public record MarketPrice(int MarketId, Price Price);