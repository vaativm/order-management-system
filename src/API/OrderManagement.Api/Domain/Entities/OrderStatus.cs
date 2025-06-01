namespace OrderManagement.Api.Domain.Entities;

public class OrderStatus
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public OrderState State { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Order Order { get; set; }
}

public enum OrderState
{
    Created,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}
