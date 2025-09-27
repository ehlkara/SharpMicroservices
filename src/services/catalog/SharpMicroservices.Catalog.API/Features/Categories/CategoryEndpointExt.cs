using SharpMicroservices.Catalog.API.Features.Categories.Create;
using SharpMicroservices.Catalog.API.Features.Categories.GetAll;

namespace SharpMicroservices.Catalog.API.Features.Categories;

public static class CategoryEndpointExt
{
    public static void AddCategoryGroupEndpointExt(this WebApplication app)
    {
        app.MapGroup("api/categories").CreateCategoryGroupItemEndpoint()
            .GetAllCategoryGroupItemEndpoint();
    }
}
