using SharpMicroservices.Catalog.API.Features.Courses.Dtos;

namespace SharpMicroservices.Catalog.API.Features.Courses.GetAllByUserId;

public record GetAllByUserIdQuery(Guid Id) : IRequestByServiceResult<List<CourseDto>>;

public class GetAllByUserIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAllByUserIdQuery, ServiceResult<List<CourseDto>>>
{
    public async Task<ServiceResult<List<CourseDto>>> Handle(GetAllByUserIdQuery request, CancellationToken cancellationToken)
    {
        var courses = await context.Courses.Where(x => x.UserId == request.Id).ToListAsync(cancellationToken);

        var categories = await context.Categories.ToListAsync(cancellationToken);

        foreach (var course in courses)
        {
            course.Category = categories.First(c => c.Id == course.CategoryId);
        }

        var courseDtos = mapper.Map<List<CourseDto>>(courses);
        return ServiceResult<List<CourseDto>>.SuccessAsOk(courseDtos);
    }
}

public static class GetAllByUserIdEndpoint
{
    public static RouteGroupBuilder GetAllByUserIdGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/user/{userId:guid}", async (IMediator mediator, Guid userId) => (await mediator.Send(new GetAllByUserIdQuery(userId))).ToGenericResult())
            .WithName("GetAllByUserId");

        return group;
    }
}