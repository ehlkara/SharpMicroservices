using SharpMicroservices.Catalog.API.Features.Courses.Create;
using SharpMicroservices.Catalog.API.Features.Courses.Dtos;

namespace SharpMicroservices.Catalog.API.Features.Courses;

public class CourseMapping : Profile
{
    public CourseMapping()
    {
        CreateMap<CreateCourseCommand, Course>().ReverseMap();
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<Feature, FeatureDto>().ReverseMap();
    }
}
