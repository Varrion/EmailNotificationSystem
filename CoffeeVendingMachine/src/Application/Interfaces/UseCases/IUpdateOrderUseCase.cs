using CoffeeVendingMachine.Application.Models.Dtos;
using CoffeeVendingMachine.Domain.Entities;

namespace CoffeeVendingMachine.Application.Interfaces.UseCases;
public interface IUpdateOrderUseCase
{
    Task<Order?> ExecuteAsync(int id, OrderDto orderDto);
}
