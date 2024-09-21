namespace CoffeeVendingMachine.Application.Models.Dtos;
public class CoffeeDto
{
    public required string Name { get; set; }

    public decimal Price { get; set; }

    public bool HasMilk { get; set; }

    public bool HasSugar { get; set; }

    public bool IsHot { get; set; }
}
