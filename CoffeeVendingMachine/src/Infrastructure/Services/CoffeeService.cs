using CoffeeVendingMachine.Application.Interfaces.Repositories;
using CoffeeVendingMachine.Application.Interfaces.Services;
using CoffeeVendingMachine.Application.Models.Dtos;
using CoffeeVendingMachine.Domain.Entities;
using CoffeeVendingMachine.Domain.Exceptions;

namespace CoffeeVendingMachine.Infrastructure.Services;
public class CoffeeService : ICoffeeService
{
    private readonly ICoffeeRepository _coffeeRepository;

    public CoffeeService(ICoffeeRepository coffeeRepository)
    {
        _coffeeRepository = coffeeRepository;
    }

    public async Task<IEnumerable<Coffee>> GetLocalCoffeesAsync()
    {
        return await _coffeeRepository.GetAllCoffeesAsync();
    }

    public async Task<Coffee?> GetLocalCoffeeByIdAsync(int id)
    {
        return await _coffeeRepository.GetCoffeeByIdAsync(id);
    }

    public async Task AddCoffeeAsync(CoffeeDto coffeeDto)
    {
        var coffee = new Coffee
        {
            Name = coffeeDto.Name,
            HasMilk = coffeeDto.HasMilk,
            HasSugar = coffeeDto.HasSugar,
            IsHot = coffeeDto.IsHot,
            Price = coffeeDto.Price
        };

        await _coffeeRepository.AddCoffeeAsync(coffee);
    }

    public async Task UpdateCoffeeAsync(int id, CoffeeDto coffeeDto)
    {
        var coffee = await _coffeeRepository.GetCoffeeByIdAsync(id);

        if (coffee == null)
        {
            throw new EntityNotFoundException(id);
        }

        coffee.Name = coffeeDto.Name;
        coffee.HasMilk = coffeeDto.HasMilk;
        coffee.HasSugar = coffeeDto.HasSugar;
        coffee.IsHot = coffeeDto.IsHot;
        coffee.Price = coffeeDto.Price;

        await _coffeeRepository.UpdateCoffeeAsync(coffee);
    }

    public async Task DeleteCoffeeAsync(int id)
    {
        await _coffeeRepository.DeleteCoffeeAsync(id);
    }
}
