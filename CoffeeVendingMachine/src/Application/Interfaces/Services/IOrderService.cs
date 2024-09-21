using CoffeeVendingMachine.Domain.Entities;

namespace CoffeeVendingMachine.Application.Interfaces.Services;
public interface IOrderService
{
    Task<IEnumerable<Order>> GetAllOrdersAsync();

    Task<Order?> GetOrderByIdAsync(int id);

    Task DeleteOrderAsync(int id);
}
