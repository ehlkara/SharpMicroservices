namespace SharpMicroservices.Order.Domain.Entities;

public class OrderItem : BaseEntity<int>
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public decimal UnitPrice { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public void SetItem(Guid productId, string productName, decimal unitPrice)
    {
        if(!string.IsNullOrWhiteSpace(ProductName))
        {
            throw new ArgumentException("Product name cannot be empty.", nameof(productName));
        }

        if(unitPrice < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price cannot be negative.");
        }

        ProductId = productId;
        ProductName = productName;
        UnitPrice = unitPrice;
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(newPrice), "Unit price cannot be negative.");
        }
        UnitPrice = newPrice;
    }

    public void ApplyDiscount(float discountPercentage)
    {
        if (discountPercentage < 0 || discountPercentage > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(discountPercentage), "Discount percentage must be between 0 and 100.");
        }
        UnitPrice -= UnitPrice * ((decimal)discountPercentage / 100);
    }

    public bool IsSameItem(OrderItem orderItem)
    {
        return this.ProductId == orderItem.ProductId;
    }
}
