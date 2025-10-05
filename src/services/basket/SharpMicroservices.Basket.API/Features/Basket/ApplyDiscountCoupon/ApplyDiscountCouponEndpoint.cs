using MediatR;
using SharpMicroservices.Shared.Extensions;
using SharpMicroservices.Shared.Filters;

namespace SharpMicroservices.Basket.API.Features.Basket.ApplyDiscountCoupon;

public static class ApplyDiscountCouponEndpoint
{
    public static RouteGroupBuilder ApplyDiscountCouponGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPut("/apply-discount-rate", async (ApplyDiscountCouponCommand command, IMediator mediator) =>
        (await mediator.Send(command)).ToGenericResult())
            .WithName("ApplyDiscountRate")
            .MapToApiVersion(1, 0)
            .AddEndpointFilter<ValidationFilter<ApplyDiscotunCouponCommandValidator>>();

        return group;
    }
}
