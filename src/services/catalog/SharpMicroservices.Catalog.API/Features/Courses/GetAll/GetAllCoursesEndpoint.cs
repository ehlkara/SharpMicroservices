using SharpMicroservices.Catalog.API.Features.Courses.Create;
using SharpMicroservices.Catalog.API.Features.Courses.Dtos;
using SharpMicroservices.Shared.Filters;

namespace SharpMicroservices.Catalog.API.Features.Courses.GetAll;

public record GetAllCoursesQuery() : IRequestByServiceResult<List<CourseDto>>;

public class GetAllCoursesQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAllCoursesQuery, ServiceResult<List<CourseDto>>>
{
    public async Task<ServiceResult<List<CourseDto>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await context.Courses.ToListAsync(cancellationToken);

        var categories = await context.Categories.ToListAsync(cancellationToken);

        foreach (var course in courses)
        {
            course.Category = categories.First(c => c.Id == course.CategoryId);
        }

        var courseDtos = mapper.Map<List<CourseDto>>(courses);
        return ServiceResult<List<CourseDto>>.SuccessAsOk(courseDtos);
    }
}

public static class GetAllCoursesEndpoint
{
    public static RouteGroupBuilder GetAllCourseGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IMediator mediator) => (await mediator.Send(new GetAllCoursesQuery())).ToGenericResult())
            .WithName("GetAllCourse")
            .AddEndpointFilter<ValidationFilter<CreateCourseCommand>>();

        return group;
    }
}
