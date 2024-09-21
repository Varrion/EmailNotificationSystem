using System.Net;
using CoffeeVendingMachine.Application.Models;
using CoffeeVendingMachine.Infrastructure.Services;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Xunit;

namespace Infrastructure.UnitTests;
public class ExternalCoffeeServiceTests
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly HttpClient _httpClient;
    private readonly ExternalCoffeeService _externalCoffeeService;

    public ExternalCoffeeServiceTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        _externalCoffeeService = new ExternalCoffeeService(_httpClient);
    }

    [Fact]
    public async Task GetExternalCoffeesAsync_ShouldReturnCoffees_WhenApiCallIsSuccessful()
    {
        // Arrange
        var expectedCoffees = new List<ExternalCoffee>
            {
                new ExternalCoffee { Id = 1, Name = "Coffee 1" },
                new ExternalCoffee { Id = 2, Name = "Coffee 2" }
            };

        var jsonResponse = JsonConvert.SerializeObject(expectedCoffees);
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse)
            });

        // Act
        var result = await _externalCoffeeService.GetExternalCoffeesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("Coffee 1", result.First().Name);
    }

    [Fact]
    public async Task GetExternalCoffeeByIdAsync_ShouldReturnCoffee_WhenApiCallIsSuccessful()
    {
        // Arrange
        var expectedCoffee = new ExternalCoffee { Id = 1, Name = "Signature Blend" };
        var jsonResponse = JsonConvert.SerializeObject(expectedCoffee);

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse)
            });

        // Act
        var result = await _externalCoffeeService.GetExternalCoffeeByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Signature Blend", result.Name);
    }

    [Fact]
    public async Task GetExternalCoffeeByIdAsync_ShouldThrowException_WhenApiCallFails()
    {
        // Arrange

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
            });

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => _externalCoffeeService.GetExternalCoffeeByIdAsync(999));
    }
}
