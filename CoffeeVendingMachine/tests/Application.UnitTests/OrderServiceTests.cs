using CoffeeVendingMachine.Application.Interfaces.Repositories;
using CoffeeVendingMachine.Domain.Entities;
using CoffeeVendingMachine.Infrastructure.Services;
using Moq;
using Xunit;

namespace CoffeeVendingMachine.Application.UnitTests;
public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _orderService = new OrderService(_orderRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllOrdersAsync_ShouldReturnListOfOrders()
    {
        // Arrange
        var orders = new List<Order> { new Order { Id = 1 }, new Order { Id = 2 } };
        _orderRepositoryMock.Setup(r => r.GetAllOrdersAsync()).ReturnsAsync(orders);

        // Act
        var result = await _orderService.GetAllOrdersAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetOrderByIdAsync_ShouldReturnOrder_WhenOrderExists()
    {
        // Arrange
        var orderId = 1;
        var order = new Order { Id = orderId };
        _orderRepositoryMock.Setup(r => r.GetOrderByIdAsync(orderId)).ReturnsAsync(order);

        // Act
        var result = await _orderService.GetOrderByIdAsync(orderId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(orderId, result.Id);
    }

    [Fact]
    public async Task DeleteOrderAsync_ShouldDeleteOrder_WhenOrderExists()
    {
        // Arrange
        var orderId = 1;
        var order = new Order { Id = orderId };
        _orderRepositoryMock.Setup(r => r.GetOrderByIdAsync(orderId)).ReturnsAsync(order);

        // Act
        await _orderService.DeleteOrderAsync(orderId);

        // Assert
        _orderRepositoryMock.Verify(r => r.DeleteOrderAsync(orderId), Times.Once);
    }
}
