using CoffeeVendingMachine.Application.Interfaces.Services;
using CoffeeVendingMachine.Application.Interfaces.UseCases;
using CoffeeVendingMachine.Application.Models;

namespace CoffeeVendingMachine.Application.UseCases.ExternalCoffees;
public class FetchSingleExternalCoffeeUseCase : IFetchSingleExternalCoffeeUseCase
{
    private readonly IExternalCoffeeService _externalCoffeeService;

    public FetchSingleExternalCoffeeUseCase(IExternalCoffeeService externalCoffeeService)
    {
        _externalCoffeeService = externalCoffeeService;
    }

    public async Task<ExternalCoffee?> ExecuteAsync(int id)
    {
        var coffee = await _externalCoffeeService.GetExternalCoffeeByIdAsync(id);

        if (coffee == null)
        {
            return null;
        }

        coffee.Price += coffee.Price * 0.10m;
        return coffee;
    }
}
