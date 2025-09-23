using SharpMicroservices.Catalog.API.Features.Courses;
using SharpMicroservices.Catalog.API.Repositories;

namespace SharpMicroservices.Catalog.API.Features.Categories;

public class Category : BaseEntity
{
    public string Name { get; set; } = default!;
    public List<Course>? Courses { get; set; }
}
