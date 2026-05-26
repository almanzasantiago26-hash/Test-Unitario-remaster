using Moq;
using Xunit;

namespace UnitTesting.Tests;

public class OrderServiceTests
{
    private readonly Mock<IInventoryRepository> _inventoryMock = new();
    private readonly Mock<INotificationService> _notificationMock = new();
    private readonly OrderService _service;

    public OrderServiceTests()
    {
        _service = new OrderService(_inventoryMock.Object, _notificationMock.Object);
    }

    [Fact]
    public async Task PlaceOrder_ValidOrder_DecreasesStockAndSendsNotification()
    {
        // Arrange
        _inventoryMock.Setup(r => r.GetStock(42)).ReturnsAsync(10);
        _inventoryMock.Setup(r => r.DecreaseStock(42, 3)).Returns(Task.CompletedTask);
        _notificationMock.Setup(n => n.SendConfirmation(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.CompletedTask);

        // Act
        var order = await _service.PlaceOrder(1, 42, 3);

        // Assert
        Assert.Equal("confirmed", order.Status);
        Assert.Equal(1, order.UserId);
        Assert.Equal(42, order.ProductId);
        _inventoryMock.Verify(r => r.DecreaseStock(42, 3), Times.Once);
        _notificationMock.Verify(n => n.SendConfirmation(1, order.OrderId), Times.Once);
    }

    [Fact]
    public async Task PlaceOrder_InsufficientStock_ThrowsException()
    {
        // Arrange
        _inventoryMock.Setup(r => r.GetStock(42)).ReturnsAsync(2);

        // Act + Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => _service.PlaceOrder(1, 42, 5)
        );

        _inventoryMock.Verify(r => r.DecreaseStock(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task PlaceOrder_InvalidQuantity_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _service.PlaceOrder(1, 42, 0));
        await Assert.ThrowsAsync<ArgumentException>(() => _service.PlaceOrder(1, 42, -1));
    }

    [Fact]
    public async Task PlaceOrder_OnSuccess_NotificationServiceCalledOnce()
    {
        // Arrange
        _inventoryMock.Setup(r => r.GetStock(10)).ReturnsAsync(20);
        _inventoryMock.Setup(r => r.DecreaseStock(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.CompletedTask);
        _notificationMock.Setup(n => n.SendConfirmation(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.CompletedTask);

        // Act
        await _service.PlaceOrder(5, 10, 1);

        // Assert
        _notificationMock.Verify(n => n.SendConfirmation(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
    }
}
