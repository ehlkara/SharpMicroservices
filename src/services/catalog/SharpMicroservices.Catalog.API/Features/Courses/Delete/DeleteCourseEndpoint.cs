using SharpMicroservices.Catalog.API.Features.Courses.GetById;

namespace SharpMicroservices.Catalog.API.Features.Courses.Delete;

public record DeleteCourseCommand(Guid Id) : IRequestByServiceResult;

public class DeleteCourseCommandHandler(AppDbContext context) : IRequestHandler<DeleteCourseCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await context.Courses.FindAsync([request.Id], cancellationToken);
        if (course is null)
            return ServiceResult.ErrorAsNotFound();

        context.Courses.Remove(course);
        await context.SaveChangesAsync(cancellationToken);
        return ServiceResult.SuccessAsNoContent();
    }
}

public static class DeleteCourseEndpoint
{
    public static RouteGroupBuilder DeleteCourseGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id:guid}", async (IMediator mediator, Guid id) => (await mediator.Send(new DeleteCourseCommand(id))).ToGenericResult())
            .WithName("DeleteCourse");

        return group;
    }
}
