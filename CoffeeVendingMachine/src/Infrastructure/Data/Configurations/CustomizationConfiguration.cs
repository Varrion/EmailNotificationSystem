using CoffeeVendingMachine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeVendingMachine.Infrastructure.Data.Configurations;
public class CustomizationConfiguration : IEntityTypeConfiguration<Customization>
{
    public void Configure(EntityTypeBuilder<Customization> builder)
    {
        builder.Property(t => t.Price)
            .HasColumnType("decimal(18,2)");
    }
}
