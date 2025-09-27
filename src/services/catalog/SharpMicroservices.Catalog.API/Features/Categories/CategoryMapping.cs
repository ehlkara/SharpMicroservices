using AutoMapper;
using SharpMicroservices.Catalog.API.Features.Categories.Dtos;

namespace SharpMicroservices.Catalog.API.Features.Categories;

public class CategoryMapping : Profile
{
    public CategoryMapping()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
    }
}
