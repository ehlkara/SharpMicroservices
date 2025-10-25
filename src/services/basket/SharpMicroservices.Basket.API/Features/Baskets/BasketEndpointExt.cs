using Asp.Versioning.Builder;
using SharpMicroservices.Basket.API.Features.Baskets.AddBasketItem;
using SharpMicroservices.Basket.API.Features.Baskets.ApplyDiscountCoupon;
using SharpMicroservices.Basket.API.Features.Baskets.DeleteBasketItem;
using SharpMicroservices.Basket.API.Features.Baskets.GetBasket;
using SharpMicroservices.Basket.API.Features.Baskets.RemoveDiscountCoupon;

namespace SharpMicroservices.Basket.API.Features.Baskets;

public static class BasketEndpointExt
{
    public static void AddBasketGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        app.MapGroup("api/v{version:apiVersion}/baskets").WithTags("Baskets")
            .WithApiVersionSet(apiVersionSet)
            .AddBasketItemGroupItemEndpoint()
            .DeleteBasketItemGroupItemEndpoint()
            .GetBasketItemGroupItemEndpoint()
            .ApplyDiscountCouponGroupItemEndpoint()
            .RemoveDiscountCouponGroupItemEndpoint().RequireAuthorization();
    }
}
