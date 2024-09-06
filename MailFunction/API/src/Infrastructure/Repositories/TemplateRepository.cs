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

    public async Task AddTemplateAsync(Template template)
    {
        _context.Template.Add(template);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTemplateAsync(int id)
    {
        var template = await _context.Template.FindAsync(id);
        if (template != null)
        {
            _context.Template.Remove(template);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Template>> GetAllTemplatesAsync()
    {
        return await _context.Template.ToListAsync();
    }

    public async Task<Template?> GetTemplateByIdAsync(int id)
    {
        return await _context.Template.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Template?> GetTemplateByIdOrNameAsync(int id, string name = "")
    {
        return await _context.Template.FirstOrDefaultAsync(x => x.Id == id || x.Name == name);
    }

    public async Task UpdateTemplateAsync(Template template)
    {
        _context.Template.Update(template);
        await _context.SaveChangesAsync();
    }
}
