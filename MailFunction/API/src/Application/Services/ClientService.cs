using API.Application.Interfaces;
using API.Domain.Entities;

namespace API.Application.Services;
public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task CreateClientAsync(Client client)
    {
        await _clientRepository.AddClientAsync(client);
    }

    public async Task<Client?> GetClientByIdAsync(int id)
    {
        return await _clientRepository.GetClientByIdAsync(id);
    }

    // Read or Get all clients
    public async Task<IEnumerable<Client>> GetAllClientsAsync()
    {
        return await _clientRepository.GetAllClientsAsync();
    }

    // Update an existing client
    public async Task UpdateClientAsync(Client client)
    {
        var existingClient = await _clientRepository.GetClientByIdAsync(client.Id);
        if (existingClient != null)
        {
            existingClient.EmailAddress = client.EmailAddress;

            await _clientRepository.UpdateClientAsync(existingClient);
        }
    }

    public async Task DeleteClientAsync(int id)
    {
        await _clientRepository.DeleteClientAsync(id);
    }

    public async Task ProcessClientXml(Stream xmlStream)
    {
        // Parse the XML into client objects (domain models)
        var clients = await ParseXmlAsync(xmlStream);

        // Save clients to the repository
        foreach (var client in clients)
        {
            await _clientRepository.AddClientAsync(client);
        }
    }

    private async Task<List<Client>> ParseXmlAsync(Stream xmlStream)
    {
        // Logic for parsing XML into client objects
        // For example, using XmlReader or any other XML parsing approach
        var clients = new List<Client>();
        // Fill the list with parsed clients
        await Task.Delay(5);
        return clients;
    }
}
