using CoffeeVendingMachine.Application.Models.Dtos;
using CoffeeVendingMachine.Domain.Entities;

namespace CoffeeVendingMachine.Application.Interfaces.Services;
public interface ICoffeeService
{
    Task<IEnumerable<Coffee>> GetLocalCoffeesAsync();

    Task<Coffee?> GetLocalCoffeeByIdAsync(int id);

    Task AddCoffeeAsync(CoffeeDto coffeeDto);

    Task UpdateCoffeeAsync(int id, CoffeeDto coffeeDto);

    Task DeleteCoffeeAsync(int id);
}
