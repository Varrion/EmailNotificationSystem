using CoffeeVendingMachine.Application.Common.Interfaces;
using CoffeeVendingMachine.Application.Interfaces.Repositories;
using CoffeeVendingMachine.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeeVendingMachine.Infrastructure.Data.Repositories;
public class OrderRepository : IOrderRepository
{
    private readonly ICoffeeDbContext _context;

    public OrderRepository(ICoffeeDbContext context)
    {
        _context = context;
    }

    public async Task CreateOrderAsync(Order order)
    {
        _context.Order.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(int id)
    {
        return await _context.Order
            .Include(o => o.Coffee)
            .Include(o => o.Customizations)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _context.Order
            .Include(o => o.Coffee)
            .Include(o => o.Customizations)
            .ToListAsync();
    }

    public async Task UpdateOrderAsync(Order order)
    {
        _context.Order.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrderAsync(int id)
    {
        var order = await _context.Order.FindAsync(id);
        if (order != null)
        {
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
