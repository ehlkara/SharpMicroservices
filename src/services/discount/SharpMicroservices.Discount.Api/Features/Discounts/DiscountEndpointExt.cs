using Asp.Versioning.Builder;
using SharpMicroservices.Discount.Api.Features.Discounts.CreateDiscount;
using SharpMicroservices.Discount.Api.Features.Discounts.GetDiscountByCode;

namespace SharpMicroservices.Discount.Api.Features.Discounts;

public static class DiscountEndpointExt
{
    public static void AddDisctountGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        app.MapGroup("api/v{version:apiVersion}/discounts").WithTags("Discounts")
            .WithApiVersionSet(apiVersionSet)
            .CreateDiscountGroupItemEndpoint()
            .GetDiscountByCodeGroupItemEndpoint();
    }
}
