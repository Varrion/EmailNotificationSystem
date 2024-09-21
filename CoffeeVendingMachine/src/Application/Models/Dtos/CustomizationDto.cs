using CoffeeVendingMachine.Domain.Enums;

namespace CoffeeVendingMachine.Application.Models.Dtos;
public class CustomizationDto
{
    public required string Name { get; set; }

    public CustomizationType Type { get; set; }

    public decimal Price { get; set; }
}
