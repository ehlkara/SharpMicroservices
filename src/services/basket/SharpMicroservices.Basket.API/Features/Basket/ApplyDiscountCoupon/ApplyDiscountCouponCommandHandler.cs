using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using SharpMicroservices.Basket.API.Const;
using SharpMicroservices.Shared;
using SharpMicroservices.Shared.Services;
using System.Text.Json;

namespace SharpMicroservices.Basket.API.Features.Basket.ApplyDiscountCoupon;

public class ApplyDiscountCouponCommandHandler(IIdentityService identityService, IDistributedCache distributedCache) : IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken)
    {
        var cacheKey = String.Format(BasketConst.BasketCacheKey, identityService.GetUserId);
        var basketAsJson = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

        if (string.IsNullOrEmpty(basketAsJson))
        {
            return ServiceResult.Error("Basket not found.", System.Net.HttpStatusCode.NotFound);
        }

        var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson)!;

        if (!basket.Items.Any())
        {
            return ServiceResult.Error("Basket is empty, you can not apply discount.", System.Net.HttpStatusCode.BadRequest);
        }

        basket.ApplyNewDiscount(request.DiscountRate, request.Coupon);

        basketAsJson = JsonSerializer.Serialize(basket);
        await distributedCache.SetStringAsync(cacheKey, basketAsJson, cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}
