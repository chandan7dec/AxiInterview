using TradeEngine.Model;

namespace TradeEngine.Repositories;

public interface ICustomerRepository
{
    public Task Create(Customer customer, CancellationToken cancellationToken);
    public Task<Customer?> Read(int customerId, CancellationToken cancellationToken);
}