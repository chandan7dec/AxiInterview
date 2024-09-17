using Microsoft.EntityFrameworkCore;
using TradeEngine.Model;

namespace TradeEngine.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly CustomerDbContext _customerDbContext;

    public CustomerRepository()
    {
        _customerDbContext = new CustomerDbContext();
    }

    public async Task Create(Customer customer, CancellationToken cancellationToken)
    {
        await _customerDbContext.Customers.AddAsync(customer, cancellationToken);
        await _customerDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Customer?> Read(int customerId, CancellationToken cancellationToken)
    {
        return await _customerDbContext.Customers.SingleOrDefaultAsync(x => x.Id == customerId, cancellationToken);
    }
}