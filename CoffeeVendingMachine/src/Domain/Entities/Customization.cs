using System.ComponentModel.DataAnnotations;

namespace CoffeeVendingMachine.Domain.Entities;
public class Customization
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public CustomizationType Type { get; set; }
    public decimal Price { get; set; }
}
