using API.Application.Interfaces;
using API.Application.UseCases;
using API.Infrastructure.Data;
using API.Infrastructure.Email;
using API.Infrastructure.Parsers;
using API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        // Setup Entity Framework with a SQL Server connection
        services.AddDbContext<EmailServiceDbContext>(options =>
            options.UseSqlServer("YourConnectionStringHere"));

        services.AddScoped<IEmailSender>(provider => new MockEmailSenderService("mock_email_log.txt"));
        services.AddScoped<IEmailSender, EmailSenderService>();

        // Register repositories
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ITemplateRepository, TemplateRepository>();
        services.AddScoped<IXmlParser, XmlParser>();
        services.AddScoped<SendEmailUseCase>();

        return services;
    }
}
