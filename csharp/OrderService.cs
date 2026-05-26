namespace UnitTesting;

public interface IInventoryRepository
{
    Task<int> GetStock(int productId);
    Task DecreaseStock(int productId, int quantity);
}

public interface INotificationService
{
    Task SendConfirmation(int userId, int orderId);
}

public record Order(int OrderId, int UserId, int ProductId, int Quantity, string Status);

public class OrderService
{
    private readonly IInventoryRepository _inventory;
    private readonly INotificationService _notification;
    private int _nextOrderId = 1;

    public OrderService(IInventoryRepository inventory, INotificationService notification)
    {
        _inventory = inventory;
        _notification = notification;
    }

    public async Task<Order> PlaceOrder(int userId, int productId, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("La cantidad debe ser mayor a 0.");

        int stock = await _inventory.GetStock(productId);

        if (stock < quantity)
            throw new InvalidOperationException("Stock insuficiente.");

        await _inventory.DecreaseStock(productId, quantity);

        int orderId = _nextOrderId++;
        await _notification.SendConfirmation(userId, orderId);

        return new Order(orderId, userId, productId, quantity, "confirmed");
    }
}
