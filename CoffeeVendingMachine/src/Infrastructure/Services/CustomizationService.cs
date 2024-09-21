using CoffeeVendingMachine.Application.Interfaces.Repositories;
using CoffeeVendingMachine.Application.Interfaces.Services;
using CoffeeVendingMachine.Application.Models.Dtos;
using CoffeeVendingMachine.Domain.Entities;
using CoffeeVendingMachine.Domain.Enums;
using CoffeeVendingMachine.Domain.Exceptions;

namespace CoffeeVendingMachine.Infrastructure.Services;
public class CustomizationService : ICustomizationService
{
    private readonly ICustomizationRepository _customizationRepository;

    public CustomizationService(ICustomizationRepository customizationRepository)
    {
        _customizationRepository = customizationRepository;
    }

    public Coffee ApplyCustomizations(Coffee coffee, IEnumerable<Customization> customizations)
    {
        foreach (var customization in customizations)
        {
            switch (customization.Type)
            {
                case CustomizationType.ExtraSugar:
                    coffee.HasSugar = true;
                    break;
                case CustomizationType.ExtraMilk:
                    coffee.HasMilk = true;
                    break;
                case CustomizationType.Caramel:
                    // Maybe add caramel to the coffee's description
                    break;
                case CustomizationType.Creamer:
                    // Apply creamer logic here
                    break;
            }
            // Add extra price for the customization
            coffee.Price += customization.Price;
        }
        return coffee;
    }

    public async Task<List<Customization>> GetAllCustomizationsAsync()
    {
        return await _customizationRepository.GetAllCustomizationsAsync();
    }

    public async Task<Customization?> GetCustomizationByIdAsync(int id)
    {
        return await _customizationRepository.GetCustomizationByIdAsync(id);
    }

    public async Task AddCustomizationAsync(CustomizationDto customizationDto)
    {
        var customization = new Customization
        {
            Name = customizationDto.Name,
            Type = customizationDto.Type,
            Price = customizationDto.Price
        };

        await _customizationRepository.AddCustomizationAsync(customization);
    }

    public async Task UpdateCustomizationAsync(int id, CustomizationDto customizationDto)
    {
        var customization = await _customizationRepository.GetCustomizationByIdAsync(id);
        if (customization == null)
        {
            throw new EntityNotFoundException(id);
        }

        customization.Name = customizationDto.Name;
        customization.Type = customizationDto.Type;
        customization.Price = customizationDto.Price;

        await _customizationRepository.UpdateCustomizationAsync(customization);
    }

    public async Task DeleteCustomizationAsync(int id)
    {
        await _customizationRepository.DeleteCustomizationAsync(id);
    }
}
