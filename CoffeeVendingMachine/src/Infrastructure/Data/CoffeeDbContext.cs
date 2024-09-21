using System.Reflection;
using CoffeeVendingMachine.Application.Common.Interfaces;
using CoffeeVendingMachine.Domain.Entities;
using CoffeeVendingMachine.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoffeeVendingMachine.Infrastructure.Data;
public class CoffeeDbContext : IdentityDbContext<ApplicationUser>, ICoffeeDbContext
{
    public CoffeeDbContext(DbContextOptions<CoffeeDbContext> options) : base(options) { }

    public DbSet<Coffee> Coffee { get; set; }

    public DbSet<Order> Order { get; set; }

    public DbSet<Customization> Customization { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
