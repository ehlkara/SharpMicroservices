using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using SharpMicroservices.Basket.API.Const;
using SharpMicroservices.Shared;
using SharpMicroservices.Shared.Extensions;
using SharpMicroservices.Shared.Services;
using System.Text.Json;

namespace SharpMicroservices.Basket.API.Features.Basket.RemoveDiscountCoupon;

public record RemoveDiscountCommand : IRequestByServiceResult;
public class RemoveDiscountCommandHandler(IIdentityService identityService, IDistributedCache distributedCache) : IRequestHandler<RemoveDiscountCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(RemoveDiscountCommand request, CancellationToken cancellationToken)
    {
        var cacheKey = String.Format(BasketConst.BasketCacheKey, identityService.GetUserId);
        var basketAsJson = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

        if (string.IsNullOrEmpty(basketAsJson))
        {
            return ServiceResult.Error("Basket not found.", System.Net.HttpStatusCode.NotFound);
        }

        var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson)!;

        basket.ClearDiscount();

        basketAsJson = JsonSerializer.Serialize(basket);
        await distributedCache.SetStringAsync(cacheKey, basketAsJson, cancellationToken);
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
