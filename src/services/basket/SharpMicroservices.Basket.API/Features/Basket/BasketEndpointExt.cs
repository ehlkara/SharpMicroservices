using Asp.Versioning.Builder;
using SharpMicroservices.Basket.API.Features.Basket.AddBasketItem;
using SharpMicroservices.Basket.API.Features.Basket.ApplyDiscountCoupon;
using SharpMicroservices.Basket.API.Features.Basket.DeleteBasketItem;
using SharpMicroservices.Basket.API.Features.Basket.GetBasket;
using SharpMicroservices.Basket.API.Features.Basket.RemoveDiscountCoupon;

namespace SharpMicroservices.Basket.API.Features.Basket;

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
            .RemoveDiscountCouponGroupItemEndpoint();
    }
}
