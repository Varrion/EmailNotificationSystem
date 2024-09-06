using API.Application.Dto;
using API.Application.Factories;
using API.Application.Interfaces;
using API.Application.Strategies;
using API.Application.UseCases;
using API.Infrastructure.Data;
using API.Infrastructure.Email;
using API.Infrastructure.Parsers;
using API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.Configure<SenderDto>(configuration.GetSection("TomiMedia"));

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        // Setup Entity Framework with a SQL Server connection
        services.AddDbContext<EmailServiceDbContext>(options =>
            options.UseSqlServer("YourConnectionStringHere"));

        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<SenderDto>>().Value);

        services.AddScoped<IEmailProcessingStrategy, SkipDbEmailProcessingStrategy>();
        services.AddScoped<IEmailProcessingStrategy, CheckDbEmailProcessingStrategy>();

        // Register factory
        services.AddScoped<IEmailProcessingStrategyFactory, EmailProcessingStrategyFactory>();


        services.AddScoped<IEmailSender, MockEmailSenderService>();

        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ITemplateRepository, TemplateRepository>();
        services.AddScoped<IXmlParser, XmlParser>();
        services.AddScoped<IBulkSendEmailUseCase, BulkSendEmailUseCase>();

        return services;
    }
}
