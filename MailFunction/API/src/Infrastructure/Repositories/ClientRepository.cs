using API.Application.Interfaces;
using API.Domain.Entities;
using API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repositories;
public class ClientRepository : IClientRepository
{
    private readonly EmailDbContext _context;

    public ClientRepository(EmailDbContext context)
    {
        _context = context;
    }

    public async Task AddClientAsync(Client client)
    {
        _context.Client.Add(client);
        await _context.SaveChangesAsync();
    }

    public async Task<Client?> GetClientByIdAsync(int id)
    {
        return await _context.Client.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Client>> GetAllClientsAsync()
    {
        return await _context.Client.ToListAsync();
    }

    public async Task UpdateClientAsync(Client client)
    {
        _context.Client.Update(client);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteClientAsync(int id)
    {
        var client = await _context.Client.FindAsync(id);
        if (client != null)
        {
            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
        }
    }
}
