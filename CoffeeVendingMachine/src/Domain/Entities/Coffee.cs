namespace CoffeeVendingMachine.Domain.Entities;
public class Coffee : BaseCoffee
{
    public bool HasMilk { get; set; }

    public bool HasSugar { get; set; }

    public bool IsHot { get; set; }
}
