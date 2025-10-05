using MediatR;
using SharpMicroservices.Shared;
using System.Text.Json;

namespace SharpMicroservices.Basket.API.Features.Baskets.ApplyDiscountCoupon;

public class ApplyDiscountCouponCommandHandler(BasketService basketService) : IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken)
    {
        var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

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

        await basketService.CreateBasketCacheAsync(basket, cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}
