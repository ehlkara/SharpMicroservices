using Asp.Versioning.Builder;
using SharpMicroservices.Basket.API.Features.Basket.AddBasketItem;
using SharpMicroservices.Basket.API.Features.Basket.DeleteBasketItem;

namespace SharpMicroservices.Basket.API.Features.Basket;

public static class BasketEndpointExt
{
    public static void AddBasketGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        app.MapGroup("api/v{version:apiVersion}/baskets").WithTags("Baskets")
            .WithApiVersionSet(apiVersionSet)
            .AddBasketItemGroupItemEndpoint()
            .DeleteBasketItemGroupItemEndpoint();
    }
}
