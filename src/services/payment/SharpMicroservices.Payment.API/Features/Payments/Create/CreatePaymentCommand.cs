using SharpMicroservices.Shared;

namespace SharpMicroservices.Payment.API.Features.Payments.Create;

public record CreatePaymentCommand(string OrderCode, string CardNumber, string CardHolderName, string CardExpirationDate, string CardSecurityNumber, decimal Amount) : IRequestByServiceResult<CreatePaymentResponse>;

public record CreatePaymentResponse(Guid PaymentId, bool Status, string? ErrorMessage);