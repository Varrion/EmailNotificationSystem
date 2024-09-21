using CoffeeVendingMachine.Application.Interfaces.Repositories;
using CoffeeVendingMachine.Application.Interfaces.Services;
using CoffeeVendingMachine.Application.Interfaces.UseCases;
using CoffeeVendingMachine.Application.Models.Dtos;
using CoffeeVendingMachine.Domain.Entities;

namespace CoffeeVendingMachine.Application.UseCases.Orders;
public class CreateOrderUseCase : BaseOrderUseCase, ICreateOrderUseCase
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderUseCase(
         IOrderRepository orderRepository,
         ICoffeeRepository coffeeRepository,
         IExternalCoffeeService externalCoffeeService,
         ICustomizationRepository customizationRepository)
         : base(coffeeRepository, externalCoffeeService, customizationRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Order> ExecuteAsync(OrderDto orderDto)
    {
        var newOrder = new Order
        {
            Type = orderDto.Type,
            OrderDate = DateTime.UtcNow
        };

        var preparedOrder = await PrepareOrderAsync(orderDto, newOrder);
        await _orderRepository.CreateOrderAsync(preparedOrder);
        return preparedOrder;
    }
}
