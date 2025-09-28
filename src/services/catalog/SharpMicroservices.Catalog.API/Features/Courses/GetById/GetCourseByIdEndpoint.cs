using SharpMicroservices.Catalog.API.Features.Courses.Dtos;

namespace SharpMicroservices.Catalog.API.Features.Courses.GetById;

public record GetCourseByIdQuery(Guid Id) : IRequestByServiceResult<CourseDto>;

public class GetCourseByIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCourseByIdQuery, ServiceResult<CourseDto>>
{
    public async Task<ServiceResult<CourseDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var hasCourse = await context.Courses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (hasCourse is null)
            return ServiceResult<CourseDto>.Error("Course not found", $"The category with Id({request.Id}) was not found", HttpStatusCode.NotFound);

        var category = await context.Categories.FindAsync(hasCourse.CategoryId, cancellationToken);
        hasCourse.Category = category!;

        var categoryDto = mapper.Map<CourseDto>(hasCourse);
        return ServiceResult<CourseDto>.SuccessAsOk(categoryDto);
    }
}

public static class GetCourseByIdEndpoint
{
    public static RouteGroupBuilder GetCourseByIdGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:guid}", async (IMediator mediator, Guid id) => (await mediator.Send(new GetCourseByIdQuery(id))).ToGenericResult())
            .WithName("GetCourseById").MapToApiVersion(1, 0);

        return group;
    }
}