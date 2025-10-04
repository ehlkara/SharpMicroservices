using Asp.Versioning.Builder;
using SharpMicroservices.Basket.API.Features.Basket.AddBasketItem;

namespace SharpMicroservices.Basket.API.Features.Basket;

public static class BasketEndpointExt
{
    public static void AddBasketGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        app.MapGroup("api/v{version:apiVersion}/baskets").WithTags("Baskets")
            .WithApiVersionSet(apiVersionSet)
            .AddBasketItemGroupItemEndpoint();
    }
}
