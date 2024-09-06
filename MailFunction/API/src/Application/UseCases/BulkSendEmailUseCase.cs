using API.Application.Enums;
using API.Application.Interfaces;

namespace API.Application.UseCases;
public class BulkSendEmailUseCase(IEmailProcessingStrategyFactory strategyFactory) : IBulkSendEmailUseCase
{
    public async Task ExecuteAsync(Stream xmlStream, EmailXMLVerificationType verificationType)
    {
        var strategy = strategyFactory.GetStrategy(verificationType);
        await strategy.ExecuteAsync(xmlStream);
    }
}
