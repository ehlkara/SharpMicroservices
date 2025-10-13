using MassTransit;

namespace SharpMicroservices.Payment.API.Repositories;

public class Payment
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string OrderCode { get; set; }
    public DateTime Created { get; set; }
    public decimal Amount { get; set; }
    public PaymentStatus PaymentStatus { get; set; }

    public Payment(Guid userId, string orderCode, decimal amount) 
    {
        Create(userId, orderCode, amount);
    }

    public void Create(Guid userId, string orderCode, decimal amount)
    {
        Id = NewId.NextSequentialGuid();
        UserId = userId;
        OrderCode = orderCode;
        Created = DateTime.UtcNow;
        Amount = amount;
        PaymentStatus = PaymentStatus.Pending;
    }

    public void UpdateStatus(PaymentStatus status)
    {
        PaymentStatus = status;
    }
}

public enum PaymentStatus
{
    Pending = 1,
    Completed,
    Failed
}
