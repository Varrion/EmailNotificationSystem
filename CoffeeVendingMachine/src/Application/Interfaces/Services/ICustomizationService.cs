using CoffeeVendingMachine.Application.Models.Dtos;
using CoffeeVendingMachine.Domain.Entities;

namespace CoffeeVendingMachine.Application.Interfaces.Services;
public interface ICustomizationService
{
    Coffee ApplyCustomizations(Coffee coffee, IEnumerable<Customization> customizations);

    Task<List<Customization>> GetAllCustomizationsAsync();
    
    Task<Customization?> GetCustomizationByIdAsync(int id);

    Task AddCustomizationAsync(CustomizationDto customization);

    Task UpdateCustomizationAsync(int id, CustomizationDto customization);

    Task DeleteCustomizationAsync(int id);
}
