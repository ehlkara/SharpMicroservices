using Asp.Versioning.Builder;
using SharpMicroservices.Catalog.API.Features.Categories.Create;
using SharpMicroservices.Catalog.API.Features.Categories.GetAll;
using SharpMicroservices.Catalog.API.Features.Categories.GetById;

namespace SharpMicroservices.Catalog.API.Features.Categories;

public static class CategoryEndpointExt
{
    public static void AddCategoryGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        app.MapGroup("api/v{version:apiVersion}/categories").WithTags("Categories")
            .WithApiVersionSet(apiVersionSet)
            .CreateCategoryGroupItemEndpoint()
            .GetAllCategoryGroupItemEndpoint()
            .GetCategoryByIdGroupItemEndpoint();
    }
}
