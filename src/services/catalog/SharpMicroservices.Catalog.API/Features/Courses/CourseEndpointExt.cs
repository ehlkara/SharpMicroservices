using Asp.Versioning.Builder;
using SharpMicroservices.Catalog.API.Features.Courses.Create;
using SharpMicroservices.Catalog.API.Features.Courses.Delete;
using SharpMicroservices.Catalog.API.Features.Courses.GetAll;
using SharpMicroservices.Catalog.API.Features.Courses.GetAllByUserId;
using SharpMicroservices.Catalog.API.Features.Courses.GetById;
using SharpMicroservices.Catalog.API.Features.Courses.Update;

namespace SharpMicroservices.Catalog.API.Features.Courses;

public static class CourseEndpointExt
{
    public static void AddCourseGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        app.MapGroup("api/v{version:apiVersion}/courses").WithTags("Courses")
            .WithApiVersionSet(apiVersionSet)
            .CreateCourseGroupItemEndpoint()
            .GetAllCourseGroupItemEndpoint()
            .GetCourseByIdGroupItemEndpoint()
            .UpdateCourseGroupItemEndpoint()
            .DeleteCourseGroupItemEndpoint()
            .GetAllByUserIdGroupItemEndpoint();
    }
}
