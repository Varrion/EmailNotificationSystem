using API.Domain.Entities;

namespace API.Application.Interfaces;
public interface IClientRepository
{
    Task<Client?> GetClientByIdAsync(int id);

    Task AddClientAsync(Client client);

    Task<IEnumerable<Client>> GetAllClientsAsync();

    Task UpdateClientAsync(Client client);

    Task DeleteClientAsync(int id);
}
