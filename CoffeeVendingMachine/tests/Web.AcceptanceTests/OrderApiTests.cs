using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CoffeeVendingMachine.Web.AcceptanceTests;
public class OrderApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public OrderApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetOrderById_ShouldReturnOrder_WhenOrderExists()
    {
        // Arrange
        var orderId = 1;  // Assuming this exists in seeded data

        // Act
        var response = await _client.GetAsync($"/api/orders/{orderId}");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.Contains("1", responseString); // Check if order with ID 1 is returned
    }

    [Fact]
    public async Task GetCoffeeById_ShouldReturnCoffee_WhenCoffeeExists()
    {
        // Arrange
        var coffeeId = 1;  // Assuming this exists in seeded data

        // Act
        var response = await _client.GetAsync($"/api/coffee/external-coffees/{coffeeId}");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.Contains("1", responseString); // Check if order with ID 1 is returned
    }

    [Fact]
    public async Task CreateOrder_ShouldReturnCreatedOrder()
    {
        // Arrange
        var orderContent = new StringContent("{\"CoffeeId\": 1, \"TotalPrice\": 10.99}", Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/orders", orderContent);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.Contains("10.99", responseString); // Ensure price is correct
    }
}
