using CoffeeVendingMachine.Application.Models;

namespace CoffeeVendingMachine.Application.Interfaces.UseCases;
public interface IFetchExternalCoffeesUseCase
{
    Task<IEnumerable<ExternalCoffee>> ExecuteAsync();
}
