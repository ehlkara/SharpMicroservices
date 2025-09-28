using MongoDB.Driver;
using SharpMicroservices.Catalog.API.Features.Categories;
using SharpMicroservices.Catalog.API.Features.Courses;
using SharpMicroservices.Catalog.API.Options;

namespace SharpMicroservices.Catalog.API.Repositories;

public static class SeedData
{
    public static async Task AddSeedDataExt(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        dbContext.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;

        if (!await dbContext.Categories.AnyAsync())
        {
            var categories = new List<Category>
            {
                new Category { Id = NewId.NextSequentialGuid(), Name = "Development" },
                new Category { Id = NewId.NextSequentialGuid(), Name = "Business" },
                new Category { Id = NewId.NextSequentialGuid(), Name = "IT & Software" },
                new Category { Id = NewId.NextSequentialGuid(), Name = "Design" },
                new Category { Id = NewId.NextSequentialGuid(), Name = "Marketing" },
                new Category { Id = NewId.NextSequentialGuid(), Name = "Personal Development" }
            };

            await dbContext.Categories.AddRangeAsync(categories);
            await dbContext.SaveChangesAsync();
        }

        if (!await dbContext.Courses.AnyAsync())
        {
            var category = await dbContext.Categories.FirstAsync();
            var randomUserId = NewId.NextGuid();
            List<Course> courses = [
                new Course
                {
                    Id = NewId.NextSequentialGuid(),
                    Name = "Learn ASP.NET Core",
                    Description = "Comprehensive guide to ASP.NET Core development.",
                    Price = 49.99M,
                    UserId = randomUserId,
                    CategoryId = category.Id,
                    Created = DateTime.UtcNow,
                    Feature = new Feature
                    {
                        Duration = 10,
                        EducatorFullName = "John Doe",
                        Rating = 4,
                    }
                },
                new Course
                {
                    Id = NewId.NextSequentialGuid(),
                    Name = "Mastering Entity Framework Core",
                    Description = "In-depth course on Entity Framework Core.",
                    Price = 39.99M,
                    UserId = randomUserId,
                    CategoryId = category.Id,
                    Created = DateTime.UtcNow,
                    Feature = new Feature
                    {
                        Duration = 8,
                        EducatorFullName = "Jane Smith",
                        Rating = 5,
                    }
                },
                new Course
                {
                    Id = NewId.NextSequentialGuid(),
                    Name = "Building RESTful APIs with ASP.NET Core",
                    Description = "Learn how to create RESTful APIs using ASP.NET Core.",
                    Price = 59.99M,
                    UserId = randomUserId,
                    CategoryId = category.Id,
                    Created = DateTime.UtcNow,
                    Feature = new Feature
                    {
                        Duration = 12,
                        EducatorFullName = "Alice Johnson",
                        Rating = 4,
                    }
                }
                ];

            await dbContext.Courses.AddRangeAsync(courses);
            await dbContext.SaveChangesAsync();
        }
    }
}
