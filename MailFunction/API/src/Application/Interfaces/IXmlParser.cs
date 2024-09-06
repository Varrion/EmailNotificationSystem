using API.Application.Dto;

namespace API.Application.Interfaces;
public interface IXmlParser
{
    Task<List<ClientIdTemplateIdDto>> ParseClientTemplateIdFromXmlAsync(Stream xmlStream);

    Task<List<ClientMarketingDataDto>> ParseClientTemplateFromXmlAsync(Stream xmlStream);
}
