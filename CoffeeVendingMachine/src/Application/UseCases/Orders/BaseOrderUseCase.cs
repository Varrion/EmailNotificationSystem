using CoffeeVendingMachine.Application.Interfaces.Repositories;
using CoffeeVendingMachine.Application.Interfaces.Services;
using CoffeeVendingMachine.Application.Interfaces.UseCases;
using CoffeeVendingMachine.Application.Models.Dtos;
using CoffeeVendingMachine.Domain.Entities;
using CoffeeVendingMachine.Domain.Enums;

namespace CoffeeVendingMachine.Application.UseCases.Orders;
public abstract class BaseOrderUseCase
{
    private readonly ICoffeeRepository _coffeeRepository;
    private readonly IExternalCoffeeService _externalCoffeeService;
    private readonly ICustomizationRepository _customizationRepository;

    public BaseOrderUseCase(
        ICoffeeRepository coffeeRepository,
        IExternalCoffeeService externalCoffeeService,
        ICustomizationRepository customizationRepository)
    {
        _coffeeRepository = coffeeRepository;
        _externalCoffeeService = externalCoffeeService;
        _customizationRepository = customizationRepository;
    }

    protected async Task<Order> PrepareOrderAsync(OrderDto orderDto, Order order)
    {
        // Reset customizations and pricing
        order.Customizations.Clear();
        order.TotalPrice = 0;

        // Fetch local or external coffee based on type
        if (orderDto.Type == CoffeeType.Local && orderDto.CoffeeId.HasValue)
        {
            var coffee = await _coffeeRepository.GetCoffeeByIdAsync(orderDto.CoffeeId.Value);
            if (coffee != null)
            {
                order.CoffeeId = coffee.Id;
                order.TotalPrice = coffee.Price;
            }
        }
        else if (orderDto.Type == CoffeeType.External && orderDto.ExternalCoffeeId.HasValue)
        {
            var externalCoffee = await _externalCoffeeService.GetExternalCoffeeByIdAsync(orderDto.ExternalCoffeeId.Value);
            if (externalCoffee != null)
            {
                order.ExternalCoffeeId = externalCoffee.Id;
                order.TotalPrice = (decimal)externalCoffee.Price;
            }
        }

        // Fetch valid customizations based on CustomizationIds
        if (orderDto.CustomizationIds.Any())
        {
            var customizations = await _customizationRepository.GetCustomizationsByIdsAsync(orderDto.CustomizationIds);
            order.Customizations = customizations;

            // Calculate the total price with customizations
            var customizationCost = customizations.Sum(c => c.Price);
            order.TotalPrice += customizationCost;
        }

        return order;
    }
}
