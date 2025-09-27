
namespace SharpMicroservices.Catalog.API.Features.Courses.Create;

public class CreateCourseCommandHandler(AppDbContext context, IMapper mapper) : IRequestHandler<CreateCourseCommand, ServiceResult<Guid>>
{
    public async Task<ServiceResult<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var hasCategory = await context.Categories.AnyAsync(x => x.Id == request.CategoryId, cancellationToken);

        if (!hasCategory)
        {
            return ServiceResult<Guid>.Error("Category not found.", $"The category with id({request.CategoryId}) was not found", HttpStatusCode.NotFound);
        }

        var hasCourse = await context.Courses.AnyAsync(x => x.Name == request.Name, cancellationToken);

        if (hasCourse)
            return ServiceResult<Guid>.Error("Course with the same name already exists.", $"The course name {request.Name} already exist.", HttpStatusCode.BadRequest);

        var newCourse = mapper.Map<Course>(request);
        newCourse.Created = DateTime.UtcNow;
        newCourse.Id = NewId.NextSequentialGuid();
        newCourse.Feature = new Feature
        {
            Duration = 10,
            Rating = 0,
            EducatorFullName = "Ehlullah Karakurt"
        };

        await context.Courses.AddAsync(newCourse);
        await context.SaveChangesAsync(cancellationToken);

        return ServiceResult<Guid>.SuccessAsCreated(newCourse.Id, $"/api/courses/{newCourse.Id}");
    }
}
