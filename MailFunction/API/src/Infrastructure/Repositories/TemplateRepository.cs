using API.Application.Interfaces;
using API.Domain.Entities;
using API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repositories;
public class TemplateRepository : ITemplateRepository
{
    private readonly EmailDbContext _context;

    public TemplateRepository(EmailDbContext context)
    {
        _context = context;
    }

    public async Task<Template?> GetTemplateByIdAsync(int id)
    {
        return await _context.Template.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Template?> GetTemplateByIdOrNameAsync(int id, string name = "")
    {
        return await _context.Template.FirstOrDefaultAsync(x => x.Id == id || x.Name == name);
    }
}
