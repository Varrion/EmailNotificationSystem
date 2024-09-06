using API.Domain.Entities;

namespace API.Application.Interfaces;
public interface ITemplateRepository
{
    Task<Template?> GetTemplateByIdAsync(int id);
    Task<Template?> GetTemplateByIdOrNameAsync(int id, string name = "");
    Task AddTemplateAsync(Template template);            // Create
    Task<IEnumerable<Template>> GetAllTemplatesAsync();  // Read (All)
    Task UpdateTemplateAsync(Template template);         // Update
    Task DeleteTemplateAsync(int id);                    // Delete
}
