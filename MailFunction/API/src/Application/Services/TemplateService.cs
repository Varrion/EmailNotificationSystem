using API.Application.Interfaces;
using API.Domain.Entities;

namespace API.Application.Services;
public class TemplateService : ITemplateService
{
    private readonly ITemplateRepository _templateRepository;

    public TemplateService(ITemplateRepository templateRepository)
    {
        _templateRepository = templateRepository;
    }

    public async Task CreateTemplateAsync(Template template)
    {
        await _templateRepository.AddTemplateAsync(template);
    }

    // Read or Get a template by Id
    public async Task<Template?> GetTemplateByIdAsync(int id)
    {
        return await _templateRepository.GetTemplateByIdAsync(id);
    }

    // Read or Get all templates
    public async Task<IEnumerable<Template>> GetAllTemplatesAsync()
    {
        return await _templateRepository.GetAllTemplatesAsync();
    }

    // Update an existing template
    public async Task UpdateTemplateAsync(Template template)
    {
        var existingTemplate = await _templateRepository.GetTemplateByIdAsync(template.Id);
        if (existingTemplate != null)
        {
            // Update the template details
            existingTemplate.Name = template.Name;
            existingTemplate.MarketingData = template.MarketingData;

            await _templateRepository.UpdateTemplateAsync(existingTemplate);
        }
    }

    // Delete a template by Id
    public async Task DeleteTemplateAsync(int id)
    {
        await _templateRepository.DeleteTemplateAsync(id);
    }
}
