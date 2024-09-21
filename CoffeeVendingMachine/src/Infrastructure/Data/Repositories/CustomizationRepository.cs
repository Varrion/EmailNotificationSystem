using CoffeeVendingMachine.Application.Common.Interfaces;
using CoffeeVendingMachine.Application.Interfaces.Repositories;
using CoffeeVendingMachine.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeeVendingMachine.Infrastructure.Data.Repositories;

public class CustomizationRepository : ICustomizationRepository
{
    private readonly ICoffeeDbContext _context;

    public CustomizationRepository(ICoffeeDbContext context)
    {
        _context = context;
    }

    public async Task<List<Customization>> GetAllCustomizationsAsync()
    {
        return await _context.Customization.ToListAsync();
    }

    public async Task<List<Customization>> GetCustomizationsByIdsAsync(List<int> ids)
    {
        return await _context.Customization
            .Where(x => ids.Contains(x.Id)).ToListAsync();
    }

    public async Task<Customization?> GetCustomizationByIdAsync(int id)
    {
        return await _context.Customization.FindAsync(id);
    }

    public async Task AddCustomizationAsync(Customization customization)
    {
        _context.Customization.Add(customization);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCustomizationAsync(Customization customization)
    {
        _context.Customization.Update(customization);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCustomizationAsync(int id)
    {
        var customization = await _context.Customization.FindAsync(id);
        if (customization != null)
        {
            _context.Customization.Remove(customization);
            await _context.SaveChangesAsync();
        }
    }
}
