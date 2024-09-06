using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace API.Infrastructure.Data;
public class EmailDbContextFactory : IDesignTimeDbContextFactory<EmailDbContext>
{
    public EmailDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<EmailDbContext>();
        var connectionString = configuration.GetConnectionString("MockDatabase");

        optionsBuilder.UseSqlite(connectionString);

        return new EmailDbContext(optionsBuilder.Options);
    }
}
