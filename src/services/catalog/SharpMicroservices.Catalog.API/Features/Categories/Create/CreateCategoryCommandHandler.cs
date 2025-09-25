using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharpMicroservices.Catalog.API.Repositories;
using SharpMicroservices.Shared;
using System.Net;

namespace SharpMicroservices.Catalog.API.Features.Categories.Create;

public class CreateCategoryCommandHandler(AppDbContext context) : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>>
{
    public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var existingCategory = await context.Categories.AnyAsync(x => x.Name == request.Name, cancellationToken);

        if (existingCategory)
        {
            return ServiceResult<CreateCategoryResponse>.Error("Category with the same name already exists.", $"The category name {request.Name} already exist.", HttpStatusCode.BadRequest);
        }

        var category = new Category
        {
            Name = request.Name,
            Id = NewId.NextSequentialGuid()
        };

        await context.Categories.AddAsync(category, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(category.Id), "<empty>");
    }
}
