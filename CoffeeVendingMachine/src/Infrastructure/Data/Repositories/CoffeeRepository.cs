using CoffeeVendingMachine.Application.Common.Interfaces;
using CoffeeVendingMachine.Application.Interfaces.Repositories;
using CoffeeVendingMachine.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeeVendingMachine.Infrastructure.Data.Repositories;
public class CoffeeRepository : ICoffeeRepository
{
    private readonly ICoffeeDbContext _context;

    public CoffeeRepository(ICoffeeDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Coffee>> GetAllCoffeesAsync()
    {
        return await _context.Coffee.ToListAsync();
    }

    public async Task<Coffee?> GetCoffeeByIdAsync(int id)
    {
        return await _context.Coffee.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task AddCoffeeAsync(Coffee coffee)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCoffeeAsync(Coffee coffee)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCoffeeAsync(int id)
    {
        throw new NotImplementedException();
    }
}
