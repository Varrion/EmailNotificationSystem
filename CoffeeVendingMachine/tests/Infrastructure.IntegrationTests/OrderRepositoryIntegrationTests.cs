using CoffeeVendingMachine.Domain.Entities;
using CoffeeVendingMachine.Infrastructure.Data;
using CoffeeVendingMachine.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Infrastructure.IntegrationTests;

public class OrderRepositoryIntegrationTests
{
    private readonly CoffeeDbContext _dbContext;
    private readonly OrderRepository _orderRepository;

    public OrderRepositoryIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<CoffeeDbContext>()
            .UseInMemoryDatabase(databaseName: "TestCoffeeDb")
            .Options;

        _dbContext = new CoffeeDbContext(options);
        _orderRepository = new OrderRepository(_dbContext);

        // Seed some data into the in-memory database
        _dbContext.Order.Add(new Order { Id = 1, TotalPrice = 5.99M });
        _dbContext.SaveChanges();
    }

    [Fact]
    public async Task GetOrderByIdAsync_ShouldReturnOrder_WhenOrderExists()
    {
        // Act
        var order = await _orderRepository.GetOrderByIdAsync(1);

        // Assert
        Assert.NotNull(order);
        Assert.Equal(1, order.Id);
    }

    [Fact]
    public async Task CreateOrderAsync_ShouldAddOrderToDatabase()
    {
        // Arrange
        int orderId = 112;
        var newOrder = new Order { Id = orderId, TotalPrice = 10.99M };

        // Act
        await _orderRepository.CreateOrderAsync(newOrder);

        // Assert
        var createdOrder = await _dbContext.Order.FindAsync(orderId);
        Assert.NotNull(createdOrder);
        Assert.Equal(10.99M, createdOrder.TotalPrice);
    }
}
