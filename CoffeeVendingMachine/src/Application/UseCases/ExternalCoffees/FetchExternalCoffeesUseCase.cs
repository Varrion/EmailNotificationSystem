using CoffeeVendingMachine.Application.Interfaces.Services;
using CoffeeVendingMachine.Application.Interfaces.UseCases;
using CoffeeVendingMachine.Application.Models;

namespace CoffeeVendingMachine.Application.UseCases.ExternalCoffees;
public class FetchExternalCoffeesUseCase : IFetchExternalCoffeesUseCase
{
    private readonly IExternalCoffeeService _externalCoffeeService;

    public FetchExternalCoffeesUseCase(IExternalCoffeeService externalCoffeeService)
    {
        _externalCoffeeService = externalCoffeeService;
    }

    public async Task<IEnumerable<ExternalCoffee>> ExecuteAsync()
    {
        var externalCoffees = await _externalCoffeeService.GetExternalCoffeesAsync();

        foreach (var coffee in externalCoffees)
        {
            coffee.Price += coffee.Price * 0.10m;
        }

        return externalCoffees;
    }
}
