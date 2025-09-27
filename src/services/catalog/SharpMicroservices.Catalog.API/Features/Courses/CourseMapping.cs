using SharpMicroservices.Catalog.API.Features.Courses.Create;

namespace SharpMicroservices.Catalog.API.Features.Courses;

public class CourseMapping : Profile
{
    public CourseMapping()
    {
        CreateMap<CreateCourseCommand, Course>().ReverseMap();
    }
}
