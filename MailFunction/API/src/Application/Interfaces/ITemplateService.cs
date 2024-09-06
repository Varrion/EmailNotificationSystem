using API.Domain.Entities;

namespace API.Application.Interfaces;
public interface ITemplateService
{
    Task CreateTemplateAsync(Template template);
    Task<Template?> GetTemplateByIdAsync(int id);
    Task<IEnumerable<Template>> GetAllTemplatesAsync();
    Task UpdateTemplateAsync(Template template);
    Task DeleteTemplateAsync(int id);
}
