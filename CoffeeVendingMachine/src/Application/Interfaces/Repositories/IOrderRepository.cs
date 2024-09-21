using CoffeeVendingMachine.Domain.Entities;

namespace CoffeeVendingMachine.Application.Interfaces.Repositories;
public interface IOrderRepository
{
    Task CreateOrderAsync(Order order);
    Task<Order?> GetOrderByIdAsync(int id);
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(int id);
}
