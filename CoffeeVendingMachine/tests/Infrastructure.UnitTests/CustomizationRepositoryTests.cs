using CoffeeVendingMachine.Application.Common.Interfaces;
using CoffeeVendingMachine.Domain.Entities;
using CoffeeVendingMachine.Infrastructure.Data.Repositories;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace Infrastructure.UnitTests;
public class CustomizationRepositoryTests
{
    private readonly Mock<ICoffeeDbContext> _dbContextMock;
    private readonly CustomizationRepository _customizationRepository;

    public CustomizationRepositoryTests()
    {
        _dbContextMock = new Mock<ICoffeeDbContext>();
        _customizationRepository = new CustomizationRepository(_dbContextMock.Object);
    }

    [Fact]
    public async Task GetCustomizationByIdAsync_ShouldReturnCustomization_WhenCustomizationExists()
    {
        // Arrange
        var customizationId = 1;
        var customization = new Customization { Id = customizationId, Name = "Mochachino" };

        // Mock DbSet to return the customization
        _dbContextMock.Setup(m => m.Customization).ReturnsDbSet(new List<Customization> { customization });

        // Act
        var result = await _customizationRepository.GetCustomizationByIdAsync(customizationId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(customizationId, result.Id);
    }

    [Fact]
    public async Task CreateCustomizationAsync_ShouldAddCustomizationToDbContext()
    {
        // Arrange
        var customization = new Customization { Id = 1, Name = "Mochachino" };

        // Act
        await _customizationRepository.AddCustomizationAsync(customization);

        // Assert
        _dbContextMock.Verify(m => m.Customization.Add(customization), Times.Once);
        _dbContextMock.Verify(m => m.SaveChangesAsync(default), Times.Once);
    }
}
