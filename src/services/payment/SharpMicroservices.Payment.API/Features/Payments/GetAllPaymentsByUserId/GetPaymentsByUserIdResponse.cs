using SharpMicroservices.Payment.API.Repositories;

namespace SharpMicroservices.Payment.API.Features.Payments.GetAllPaymentsByUserId;

public record GetPaymentsByUserIdResponse(Guid Id,string OrderCode, string Amount, DateTime Created, PaymentStatus Status);
