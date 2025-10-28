namespace SharpMicroservices.Payment.API.Features.Payments.GetStatus;

public record GetPaymentStatusResponse(Guid? PaymentId, bool IsPaid);
