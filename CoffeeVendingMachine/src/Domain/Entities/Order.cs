using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeVendingMachine.Domain.Entities;
public class Order
{
    [Key]
    public int Id { get; set; }

    public int? CoffeeId { get; set; }

    [ForeignKey("CoffeeId")]
    public Coffee? Coffee { get; set; }

    public int? ExternalCoffeeId { get; set; }

    public CoffeeType Type { get; set; } = CoffeeType.Local;

    public List<Customization> Customizations { get; set; } = new List<Customization>();

    public decimal TotalPrice { get; set; }

    public DateTime OrderDate { get; set; }
}
