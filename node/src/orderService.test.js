const OrderService = require('./orderService');

// Mocks de las dependencias
const mockInventory = {
  getStock: jest.fn(),
  decreaseStock: jest.fn(),
};

const mockNotification = {
  sendConfirmation: jest.fn(),
};

describe('OrderService', () => {
  let service;

  beforeEach(() => {
    jest.clearAllMocks();
    service = new OrderService(mockInventory, mockNotification);
  });

  it('placeOrder_ValidOrder_DecreasesStockAndSendsNotification', async () => {
    // Arrange
    mockInventory.getStock.mockResolvedValue(10);
    mockInventory.decreaseStock.mockResolvedValue();
    mockNotification.sendConfirmation.mockResolvedValue();

    // Act
    const order = await service.placeOrder(1, 42, 3);

    // Assert
    expect(order).toMatchObject({ userId: 1, productId: 42, quantity: 3, status: 'confirmed' });
    expect(mockInventory.decreaseStock).toHaveBeenCalledWith(42, 3);
    expect(mockNotification.sendConfirmation).toHaveBeenCalledWith(1, order.orderId);
  });

  it('placeOrder_InsufficientStock_ThrowsException', async () => {
    // Arrange
    mockInventory.getStock.mockResolvedValue(2);

    // Act + Assert
    await expect(service.placeOrder(1, 42, 5)).rejects.toThrow('Stock insuficiente.');
    expect(mockInventory.decreaseStock).not.toHaveBeenCalled();
  });

  it('placeOrder_InvalidQuantity_ThrowsException', async () => {
    await expect(service.placeOrder(1, 42, 0)).rejects.toThrow('La cantidad debe ser mayor a 0.');
    await expect(service.placeOrder(1, 42, -1)).rejects.toThrow('La cantidad debe ser mayor a 0.');
  });

  it('placeOrder_OnSuccess_NotificationServiceCalledOnce', async () => {
    // Arrange
    mockInventory.getStock.mockResolvedValue(10);
    mockInventory.decreaseStock.mockResolvedValue();
    mockNotification.sendConfirmation.mockResolvedValue();

    // Act
    await service.placeOrder(5, 10, 1);

    // Assert
    expect(mockNotification.sendConfirmation).toHaveBeenCalledTimes(1);
  });
});
