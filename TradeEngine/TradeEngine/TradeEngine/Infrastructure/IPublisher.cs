namespace TradeEngine.Infrastructure;

public interface IPublisher<in T>
{
    void Publish(T tradeValuation);
}