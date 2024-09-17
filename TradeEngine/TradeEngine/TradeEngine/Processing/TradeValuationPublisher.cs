using System.Threading.Tasks.Dataflow;
using TradeEngine.Infrastructure;
using TradeEngine.Processing.Domain;

namespace TradeEngine.Processing;

public class TradeValuationPublisher : IPublisher<TradeValuation>
{
    private readonly ActionBlock<TradeValuation> _tradeValuationsToPublish;
    private readonly ILogger<TradeValuationPublisher> _logger;
    private int _updateCounter = 0;

    public TradeValuationPublisher(ILogger<TradeValuationPublisher> logger)
    {
        _logger = logger;
        _tradeValuationsToPublish = new ActionBlock<TradeValuation>(ProcessValuation);
    }

    private void ProcessValuation(TradeValuation obj)
    {
        if (_updateCounter++ % 100000 == 0)
        {
            _logger.LogInformation("Trade valuations published={UpdatesPublished}", _updateCounter);
        }
    }

    public void Publish(TradeValuation tradeValuation)
    {
        _tradeValuationsToPublish.Post(tradeValuation);
    }
}