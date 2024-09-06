using API.Domain.Entities;

namespace API.Application.Interfaces;
public interface ITemplateRepository
{
    Task<Template?> GetTemplateByIdAsync(int id);
    Task<Template?> GetTemplateByIdOrNameAsync(int id, string name = "");
}
