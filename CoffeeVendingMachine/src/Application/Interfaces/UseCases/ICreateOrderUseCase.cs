using CoffeeVendingMachine.Application.Models.Dtos;
using CoffeeVendingMachine.Domain.Entities;

namespace CoffeeVendingMachine.Application.Interfaces.UseCases;
public interface ICreateOrderUseCase
{
    Task<Order> ExecuteAsync(OrderDto order);
}
