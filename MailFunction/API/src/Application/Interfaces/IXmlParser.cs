using API.Application.Dto;
using API.Domain.Entities;

namespace API.Application.Interfaces;
public interface IXmlParser
{
    Task<List<Client>> ParseClientsFromXmlAsync(string filePath);

    Task<List<ClientIdTemplateIdDto>> ParseClientTemplateIdFromXmlAsync(string filePath);

    Task<List<ClientMarketingDataDto>> ParseClientTemplateFromXmlAsync(string filePath);
}
