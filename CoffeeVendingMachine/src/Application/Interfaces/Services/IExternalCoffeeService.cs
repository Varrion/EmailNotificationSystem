using CoffeeVendingMachine.Application.Models;

namespace CoffeeVendingMachine.Application.Interfaces.Services;
public interface IExternalCoffeeService
{
    Task<IEnumerable<ExternalCoffee>> GetExternalCoffeesAsync();

    Task<ExternalCoffee?> GetExternalCoffeeByIdAsync(int id);
}
