using API.Domain.Entities;

namespace API.Application.Interfaces;
public interface IXmlParser
{
    Task<List<Client>> ParseClientsFromXmlAsync(string filePath);
}
