using CoffeeVendingMachine.Domain.Entities;

namespace CoffeeVendingMachine.Application.Common.Interfaces;

public interface ICoffeeDbContext
{
    public DbSet<Coffee> Coffee { get; set; }

    public DbSet<Order> Order { get; set; }

    public DbSet<Customization> Customization { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
