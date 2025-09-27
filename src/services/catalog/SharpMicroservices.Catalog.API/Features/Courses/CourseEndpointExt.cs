using SharpMicroservices.Catalog.API.Features.Courses.Create;
using SharpMicroservices.Catalog.API.Features.Courses.GetAll;
using SharpMicroservices.Catalog.API.Features.Courses.GetById;
using SharpMicroservices.Catalog.API.Features.Courses.Update;

namespace SharpMicroservices.Catalog.API.Features.Courses;

public static class CourseEndpointExt
{
    public static void AddCourseGroupEndpointExt(this WebApplication app)
    {
        app.MapGroup("api/courses").WithTags("Courses")
            .CreateCourseGroupItemEndpoint()
            .GetAllCourseGroupItemEndpoint()
            .GetCourseByIdGroupItemEndpoint()
            .UpdateCourseGroupItemEndpoint();
    }
}
