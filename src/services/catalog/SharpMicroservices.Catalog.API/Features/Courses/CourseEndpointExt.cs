﻿using SharpMicroservices.Catalog.API.Features.Courses.Create;
using SharpMicroservices.Catalog.API.Features.Courses.GetAll;

namespace SharpMicroservices.Catalog.API.Features.Courses;

public static class CourseEndpointExt
{
    public static void AddCourseGroupEndpointExt(this WebApplication app)
    {
        app.MapGroup("api/courses").WithTags("Courses")
            .CreateCourseGroupItemEndpoint()
            .GetAllCourseGroupItemEndpoint();
    }
}
