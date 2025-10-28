using Refit;

namespace SharpMicroservices.Order.Application.Contracts.Refit.PaymentService;

public interface IPaymentService
{
    [Post("/api/v1/payments")]
    Task<CreatePaymentResponse> CreateAsync(CreatePaymentRequest paymentRequest);
}
