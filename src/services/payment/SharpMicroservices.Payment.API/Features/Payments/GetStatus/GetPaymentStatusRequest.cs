using SharpMicroservices.Shared;

namespace SharpMicroservices.Payment.API.Features.Payments.GetStatus;

public record GetPaymentStatusRequest(string orderCode) : IRequestByServiceResult<GetPaymentStatusResponse>;