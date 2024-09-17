using TradeEngine.Processing.Domain;

namespace TradeEngine.Processing;

public class TradeValuationEventArgs : EventArgs
{
    public TradeValuation Valuation { get; }

    public TradeValuationEventArgs(TradeValuation valuation)
    {
        Valuation = valuation;
    }
}