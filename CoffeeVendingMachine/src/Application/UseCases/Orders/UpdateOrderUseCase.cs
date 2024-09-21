using CoffeeVendingMachine.Application.Interfaces.Repositories;
using CoffeeVendingMachine.Application.Interfaces.Services;
using CoffeeVendingMachine.Application.Interfaces.UseCases;
using CoffeeVendingMachine.Application.Models.Dtos;
using CoffeeVendingMachine.Domain.Entities;

namespace CoffeeVendingMachine.Application.UseCases.Orders;
public class UpdateOrderUseCase : BaseOrderUseCase, IUpdateOrderUseCase
{
    private readonly IOrderRepository _orderRepository;

    public UpdateOrderUseCase(
        IOrderRepository orderRepository,
        ICoffeeRepository coffeeRepository,
        IExternalCoffeeService externalCoffeeService,
        ICustomizationRepository customizationRepository)
        : base(coffeeRepository, externalCoffeeService, customizationRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Order?> ExecuteAsync(int id, OrderDto orderDto)
    {
        var existingOrder = await _orderRepository.GetOrderByIdAsync(id);
        if (existingOrder == null)
        {
            return null;
        }

        var preparedOrder = await PrepareOrderAsync(orderDto, existingOrder);
        await _orderRepository.UpdateOrderAsync(preparedOrder);

        return preparedOrder;
    }
}
