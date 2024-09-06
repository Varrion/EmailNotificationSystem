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

    public async Task<Client?> GetClientByIdAsync(int id)
    {
        return await _context.Client
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
