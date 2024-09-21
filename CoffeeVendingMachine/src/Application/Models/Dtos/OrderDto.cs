using CoffeeVendingMachine.Domain.Enums;

namespace CoffeeVendingMachine.Application.Models.Dtos;

public class OrderDto
{
    public int? CoffeeId { get; set; }

    public int? ExternalCoffeeId { get; set; }

    public required CoffeeType Type { get; set; }

    public List<int> CustomizationIds { get; set; } = new List<int>();
}
