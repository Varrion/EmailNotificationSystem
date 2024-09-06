using API.Domain.Entities;

namespace API.Application.Interfaces
{
    public interface IClientService
    {
        Task CreateClientAsync(Client client);
        Task<Client?> GetClientByIdAsync(int id);
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(int id);
        Task ProcessClientXml(Stream xmlStream);
    }
}
