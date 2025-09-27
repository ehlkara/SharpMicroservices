using AutoMapper;
using MediatR;
using SharpMicroservices.Catalog.API.Features.Categories.Dtos;
using SharpMicroservices.Catalog.API.Repositories;
using SharpMicroservices.Shared;
using SharpMicroservices.Shared.Extensions;
using System.Net;

namespace SharpMicroservices.Catalog.API.Features.Categories.GetById;

public record GetCategoryByIdQuery(Guid Id) : IRequest<ServiceResult<CategoryDto>>;

public class GetCategoryByIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, ServiceResult<CategoryDto>>
{
    public async Task<ServiceResult<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var hasCategory = await context.Categories.FindAsync(request.Id, cancellationToken);
        if (hasCategory == null)
            return ServiceResult<CategoryDto>.Error("Category not found", $"The category with Id({request.Id}) was not found", HttpStatusCode.NotFound);

        var categoryDto = mapper.Map<CategoryDto>(hasCategory);
        return ServiceResult<CategoryDto>.SuccessAsOk(categoryDto);
    }
}

public static class GetCategoryByIdEndpoint
{
    public static RouteGroupBuilder GetCategoryByIdGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:guid}", async (IMediator mediator, Guid id) => (await mediator.Send(new GetCategoryByIdQuery(id))).ToGenericResult());

        return group;
    }
}