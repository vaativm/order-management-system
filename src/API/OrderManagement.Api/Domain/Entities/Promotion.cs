namespace OrderManagement.Api.Domain.Entities;

public class Promotion
{
    public Guid Id { get;  set; }
    public string Name { get;  set; } = string.Empty;
    public DiscountType Type { get;  set; }
    public decimal Value { get;  set; }
    public CustomerSegment CustomerSegment { get;  set; }
    public DateTime ValidFrom { get;  set; }
    public DateTime? ValidTo { get;  set; }

}

public enum DiscountType
{
    Percentage,
    FixedAmount
}

public enum CustomerSegment
{
    New,
    Regular,
    VIP
}
