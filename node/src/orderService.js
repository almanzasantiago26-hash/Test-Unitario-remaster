class OrderService {
  constructor(inventoryRepository, notificationService) {
    this.inventory = inventoryRepository;
    this.notification = notificationService;
    this._nextOrderId = 1;
  }

  async placeOrder(userId, productId, quantity) {
    if (quantity <= 0) {
      throw new Error('La cantidad debe ser mayor a 0.');
    }

    const stock = await this.inventory.getStock(productId);

    if (stock < quantity) {
      throw new Error('Stock insuficiente.');
    }

    await this.inventory.decreaseStock(productId, quantity);

    const orderId = this._nextOrderId++;
    await this.notification.sendConfirmation(userId, orderId);

    return { orderId, userId, productId, quantity, status: 'confirmed' };
  }
}

module.exports = OrderService;
