using API.Application.Dto;
using API.Application.Factories;
using API.Application.Interfaces;
using API.Application.Services;
using API.Application.Strategies;
using API.Application.UseCases;
using API.Infrastructure.Data;
using API.Infrastructure.Email;
using API.Infrastructure.Parsers;
using API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using static API.Infrastructure.Data.InitialiserExtensions;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MockDatabase");
        Guard.Against.Null(connectionString, message: "Connection string 'MockDatabase' not found.");

        services.AddDbContext<EmailDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<EmailDbContextInitialiser>();

        services.Configure<SenderDto>(configuration.GetSection("TomiMedia"));
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<SenderDto>>().Value);

        services.AddScoped<SkipDbEmailProcessingStrategy>();
        services.AddScoped<CheckDbEmailProcessingStrategy>();

        // Register factory
        services.AddScoped<IEmailProcessingStrategyFactory, EmailProcessingStrategyFactory>();

        services.AddScoped<IEmailDbContext>(provider => provider.GetRequiredService<EmailDbContext>());


        services.AddScoped<IEmailSender, MockEmailSenderService>();

        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ITemplateRepository, TemplateRepository>();
        services.AddScoped<IXmlParser, XmlParser>();
        services.AddScoped<IBulkSendEmailUseCase, BulkSendEmailUseCase>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<ITemplateService, TemplateService>();

        return services;
    }
}
