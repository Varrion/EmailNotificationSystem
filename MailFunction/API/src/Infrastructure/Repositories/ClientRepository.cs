using API.Application.Interfaces;
using API.Domain.Entities;
using API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repositories;
public class ClientRepository : IClientRepository
{
    private readonly EmailServiceDbContext _context;

    public ClientRepository(EmailServiceDbContext context)
    {
        _context = context;
    }

    public async Task<Client?> GetClientByIdAsync(int id)
    {
        return await _context.Clients
            .Include(c => c.Configuration)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
