using System.Text;

namespace SharpMicroservices.Order.Domain.Entities;

public class Order : BaseEntity<Guid>
{
    public string Code { get; set; } = null!;
    public DateTime Created { get; set; }
    public Guid BuyerId { get; set; }
    public OrderStatus Status { get; set; }
    public int AddressId { get; set; }
    public decimal TotalPrice { get; set; }
    public float? DiscountRate { get; set; }
    public Guid? PaymentId { get; set; }

    public List<OrderItem> OrderItems { get; set; } = new();
    public Address Address { get; set; } = null!;


    public static string GenerateCode()
    {
        var random = new Random();
        var orderCode = new StringBuilder(10);

        for (int i = 0; i < 10; i++)
        {
            orderCode.Append(random.Next(0, 10));
        }

        return orderCode.ToString();
    }

    public static Order CreateUnPaidOrder(Guid buyerId, float? discountRate, int addressId)
    {
        return new Order
        {
            Id = Guid.CreateVersion7(),
            Code = GenerateCode(),
            BuyerId = buyerId,
            Created = DateTime.UtcNow,
            Status = OrderStatus.WaitingForPayment,
            AddressId = addressId,
            DiscountRate = discountRate,
            TotalPrice = 0m
        };
    }

    public static Order CreateUnPaidOrder(Guid buyerId, float? discountRate)
    {
        return new Order
        {
            Id = Guid.CreateVersion7(),
            Code = GenerateCode(),
            BuyerId = buyerId,
            Created = DateTime.UtcNow,
            Status = OrderStatus.WaitingForPayment,
            DiscountRate = discountRate,
            TotalPrice = 0m
        };
    }

    public void AddOrderItem(Guid productId, string productName, decimal unitPrice)
    {
        var orderItem = new OrderItem();

        if (DiscountRate.HasValue)
        {
            unitPrice -= unitPrice * ((decimal)DiscountRate.Value / 100);
        }

        orderItem.SetItem(productId, productName, unitPrice);
        OrderItems.Add(orderItem);

        CalculateTotalPrice();
    }

    public void MarkAsPaid(Guid paymentId)
    {
        if (Status != OrderStatus.WaitingForPayment)
        {
            throw new InvalidOperationException("Only orders waiting for payment can be marked as paid.");
        }
        Status = OrderStatus.Paid;
        PaymentId = paymentId;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Paid)
        {
            throw new InvalidOperationException("Paid orders cannot be cancelled.");
        }
        Status = OrderStatus.Cancelled;
    }
    private void CalculateTotalPrice()
    {
        TotalPrice = OrderItems.Sum(oi => oi.UnitPrice);
    }
}
