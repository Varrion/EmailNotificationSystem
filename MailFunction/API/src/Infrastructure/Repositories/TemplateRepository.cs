using API.Application.Interfaces;
using API.Domain.Entities;
using API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repositories;
public class TemplateRepository : ITemplateRepository
{
    private readonly EmailServiceDbContext _context;

    public TemplateRepository(EmailServiceDbContext context)
    {
        _context = context;
    }

    public async Task<Template?> GetTemplateByIdAsync(int id)
    {
        return await _context.Templates.FirstOrDefaultAsync(x => x.Id == id);
    }
}
