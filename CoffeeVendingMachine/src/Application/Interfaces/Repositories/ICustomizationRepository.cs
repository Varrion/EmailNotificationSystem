using CoffeeVendingMachine.Domain.Entities;

namespace CoffeeVendingMachine.Application.Interfaces.Repositories;
public interface ICustomizationRepository
{
    Task<List<Customization>> GetAllCustomizationsAsync();
    Task<List<Customization>> GetCustomizationsByIdsAsync(List<int> ids);
    Task<Customization?> GetCustomizationByIdAsync(int id);
    Task AddCustomizationAsync(Customization customization);
    Task UpdateCustomizationAsync(Customization customization);
    Task DeleteCustomizationAsync(int id);
}
