using CoffeeVendingMachine.Domain.Entities;

namespace CoffeeVendingMachine.Application.Interfaces.Repositories;
public interface ICoffeeRepository
{
    Task<IEnumerable<Coffee>> GetAllCoffeesAsync();

    Task<Coffee?> GetCoffeeByIdAsync(int id);

    Task AddCoffeeAsync(Coffee coffee);

    Task UpdateCoffeeAsync(Coffee coffee);

    Task DeleteCoffeeAsync(int id);
}
