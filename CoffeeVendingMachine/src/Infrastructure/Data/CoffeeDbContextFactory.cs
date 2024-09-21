using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CoffeeVendingMachine.Infrastructure.Data;
internal class CoffeeDbContextFactory : IDesignTimeDbContextFactory<CoffeeDbContext>
{
    public CoffeeDbContext CreateDbContext(string[] args)
    {
        // Build configuration
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json") // Assumes you have appsettings.json in your startup project
            .Build();

        // Create options builder
        var builder = new DbContextOptionsBuilder<CoffeeDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Use SQL Server
        builder.UseSqlServer(connectionString);

        return new CoffeeDbContext(builder.Options);
    }
}
