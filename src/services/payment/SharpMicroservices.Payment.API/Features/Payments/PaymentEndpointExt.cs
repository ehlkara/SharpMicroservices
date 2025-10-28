using Asp.Versioning.Builder;
using SharpMicroservices.Payment.API.Features.Payments.Create;
using SharpMicroservices.Payment.API.Features.Payments.GetAllPaymentsByUserId;
using SharpMicroservices.Payment.API.Features.Payments.GetStatus;

namespace SharpMicroservices.Payment.API.Features.Payments;

public static class PaymentEndpointExt
{
    public static void AddPaymentGroupEndpointExt(this IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
    {
        app.MapGroup("api/v{version:apiversion}/payments").WithTags("payments").WithApiVersionSet(apiVersionSet)
            .CreatePaymentGroupItemEndpoint()
            .GetAllPaymentsByUserIdGroupItemEndpoint()
            .GetPaymentStatusGroupItemEndpoint();
    }
}
