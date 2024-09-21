using CoffeeVendingMachine.Application.Common.Interfaces;
using CoffeeVendingMachine.Domain.Entities;
using CoffeeVendingMachine.Infrastructure.Data.Repositories;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace Infrastructure.UnitTests;

public class OrderRepositoryTests
{
    private readonly Mock<ICoffeeDbContext> _dbContextMock;
    private readonly OrderRepository _orderRepository;

    public OrderRepositoryTests()
    {
        _dbContextMock = new Mock<ICoffeeDbContext>();
        _orderRepository = new OrderRepository(_dbContextMock.Object);
    }

    [Fact]
    public async Task GetOrderByIdAsync_ShouldReturnOrder_WhenOrderExists()
    {
        // Arrange
        var orderId = 1;
        var order = new Order { Id = orderId };

        // Mock DbSet<Order> to return the specific order for FirstOrDefaultAsync
        _dbContextMock.Setup(m => m.Order)
                      .ReturnsDbSet(new List<Order> { order });

        // Act
        var result = await _orderRepository.GetOrderByIdAsync(orderId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(orderId, result.Id);
    }

    [Fact]
    public async Task CreateOrderAsync_ShouldAddOrderToDbContext()
    {
        // Arrange
        var order = new Order { Id = 1 };

        // Mock DbSet<Order> for AddAsync behavior
        _dbContextMock.Setup(m => m.Order)
                      .ReturnsDbSet(new List<Order>());

        // Act
        await _orderRepository.CreateOrderAsync(order);

        // Assert
        _dbContextMock.Verify(m => m.Order.Add(order), Times.Once);
        _dbContextMock.Verify(m => m.SaveChangesAsync(default), Times.Once);
    }
}
