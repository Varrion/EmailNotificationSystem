using CoffeeVendingMachine.Application.Models;

namespace CoffeeVendingMachine.Application.Interfaces.UseCases;
public interface IFetchSingleExternalCoffeeUseCase
{
    Task<ExternalCoffee?> ExecuteAsync(int id);
}
