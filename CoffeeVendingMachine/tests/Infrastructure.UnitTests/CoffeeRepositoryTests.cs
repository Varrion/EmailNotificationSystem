using CoffeeVendingMachine.Application.Common.Interfaces;
using CoffeeVendingMachine.Domain.Entities;
using CoffeeVendingMachine.Infrastructure.Data.Repositories;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace Infrastructure.UnitTests;
public class CoffeeRepositoryTests
{
    private readonly Mock<ICoffeeDbContext> _dbContextMock;
    private readonly CoffeeRepository _coffeeRepository;

    public CoffeeRepositoryTests()
    {
        _dbContextMock = new Mock<ICoffeeDbContext>();
        _coffeeRepository = new CoffeeRepository(_dbContextMock.Object);
    }

    [Fact]
    public async Task GetCoffeeByIdAsync_ShouldReturnCoffee_WhenCoffeeExists()
    {
        // Arrange
        var coffeeId = 1;
        var coffee = new Coffee { Id = coffeeId, Name = "Espresso" };

        // Mock DbSet to return the coffee
        _dbContextMock.Setup(m => m.Coffee).ReturnsDbSet(new List<Coffee> { coffee });

        // Act
        var result = await _coffeeRepository.GetCoffeeByIdAsync(coffeeId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(coffeeId, result.Id);
    }

    [Fact]
    public async Task CreateCoffeeAsync_ShouldAddCoffeeToDbContext()
    {
        // Arrange
        var coffee = new Coffee { Id = 1, Name = "Espresso" };

        // Act
        await _coffeeRepository.AddCoffeeAsync(coffee);

        // Assert
        _dbContextMock.Verify(m => m.Coffee.Add(coffee), Times.Once);
        _dbContextMock.Verify(m => m.SaveChangesAsync(default), Times.Once);
    }
}
