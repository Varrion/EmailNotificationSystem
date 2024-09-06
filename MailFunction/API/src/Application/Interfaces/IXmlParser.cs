using API.Application.Dto;

namespace API.Application.Interfaces;
public interface IXmlParser
{
    Task<List<ClientIdTemplateIdDto>> ParseClientTemplateIdFromXmlAsync(string filePath);

    Task<List<ClientMarketingDataDto>> ParseClientTemplateFromXmlAsync(string filePath);
}
