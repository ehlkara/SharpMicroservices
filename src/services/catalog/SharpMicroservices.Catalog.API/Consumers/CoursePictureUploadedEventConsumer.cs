using SharpMicroservices.Bus.Events;

namespace SharpMicroservices.Catalog.API.Consumers;

public class CoursePictureUploadedEventConsumer(IServiceProvider serviceProvider) : IConsumer<CoursePictureUploadedEvent>
{
    public async Task Consume(ConsumeContext<CoursePictureUploadedEvent> context)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var course = await dbContext.Courses.FirstOrDefaultAsync(x => x.Id == context.Message.CourseId);
        if (course is not null)
        {
            course.ImageUrl = context.Message.ImageUrl;
            dbContext.Courses.Update(course);
            await dbContext.SaveChangesAsync();
        }
        else
        {
            throw new Exception($"Course with id {context.Message.CourseId} not found.");
        }
    }
}
