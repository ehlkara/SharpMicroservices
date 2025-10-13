using SharpMicroservices.Shared;

namespace SharpMicroservices.Payment.API.Features.Payments.GetAllPaymentsByUserId;

public record GetPaymentsByUserIdQuery : IRequestByServiceResult<List<GetPaymentsByUserIdResponse>>;
