using CoffeeVendingMachine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeVendingMachine.Infrastructure.Data.Configurations;
public class CoffeeConfiguration : IEntityTypeConfiguration<Coffee>
{
    public void Configure(EntityTypeBuilder<Coffee> builder)
    {
        builder.Property(t => t.Price)
            .HasColumnType("decimal(18,2)");
    }
}
