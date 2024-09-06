using API.Application.Enums;

namespace API.Application.Interfaces;
public interface IEmailProcessingStrategyFactory
{
    IEmailProcessingStrategy GetStrategy(EmailXMLVerificationType verificationType);
}
