using API.Application.Enums;
using API.Application.Interfaces;
using API.Application.Strategies;
using Microsoft.Extensions.DependencyInjection;

namespace API.Application.Factories;
public class EmailProcessingStrategyFactory : IEmailProcessingStrategyFactory
{
    private readonly IServiceProvider _serviceProvider;

    public EmailProcessingStrategyFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IEmailProcessingStrategy GetStrategy(EmailXMLVerificationType verificationType)
    {
        return verificationType switch
        {
            EmailXMLVerificationType.SkipDb => _serviceProvider.GetRequiredService<SkipDbEmailProcessingStrategy>(),
            EmailXMLVerificationType.CheckDb => _serviceProvider.GetRequiredService<CheckDbEmailProcessingStrategy>(),
            _ => throw new ArgumentException("Invalid verification type", nameof(verificationType))
        };
    }
}
