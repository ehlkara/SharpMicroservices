using MediatR;
using SharpMicroservices.Shared;
using SharpMicroservices.Shared.Extensions;
using System.Text.Json;

namespace SharpMicroservices.Basket.API.Features.Baskets.RemoveDiscountCoupon;

public record RemoveDiscountCommand : IRequestByServiceResult;
public class RemoveDiscountCommandHandler(BasketService basketService) : IRequestHandler<RemoveDiscountCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(RemoveDiscountCommand request, CancellationToken cancellationToken)
    {
        var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

        if (string.IsNullOrEmpty(basketAsJson))
        {
            return ServiceResult.Error("Basket not found.", System.Net.HttpStatusCode.NotFound);
        }

        var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson)!;

        basket.ClearDiscount();

        await basketService.CreateBasketCacheAsync(basket, cancellationToken);
        return ServiceResult.SuccessAsNoContent();
    }
}

public static class RemoveDiscountCouponEndpoint
{
    public static RouteGroupBuilder RemoveDiscountCouponGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("/remove-discount-coupon", async (IMediator mediator) =>
        (await mediator.Send(new RemoveDiscountCommand())).ToGenericResult())
            .WithName("RemoveDiscountCoupon")
            .MapToApiVersion(1, 0);

        return group;
    }
}
