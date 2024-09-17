using Microsoft.EntityFrameworkCore;

namespace TradeEngine.Model;

public class CustomerDbContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseInMemoryDatabase("CustomerDb");
        base.OnConfiguring(optionsBuilder);
    }
}