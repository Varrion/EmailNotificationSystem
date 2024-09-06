using API.Application.Enums;
using API.Application.Interfaces;

namespace API.Application.UseCases;
public class BulkSendEmailUseCase(IEmailProcessingStrategyFactory strategyFactory) : IBulkSendEmailUseCase
{
    public async Task ExecuteAsync(string xmlFilePath, EmailXMLVerificationType verificationType)
    {
        var strategy = strategyFactory.GetStrategy(verificationType);
        await strategy.ExecuteAsync(xmlFilePath);
    }
}
