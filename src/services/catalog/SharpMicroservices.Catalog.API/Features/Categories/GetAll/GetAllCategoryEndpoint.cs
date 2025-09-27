using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharpMicroservices.Catalog.API.Features.Categories.Dtos;
using SharpMicroservices.Catalog.API.Repositories;
using SharpMicroservices.Shared;
using SharpMicroservices.Shared.Extensions;

namespace SharpMicroservices.Catalog.API.Features.Categories.GetAll;

public class GetAllCategoryQuery : IRequest<ServiceResult<List<CategoryDto>>>;

public class GetAllCategoryQueryhandler(AppDbContext context, IMapper mapper) : IRequestByServiceResult<List<CategoryDto>>
{
    public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await context.Categories.ToListAsync(cancellationToken);
        var categoriesDto = mapper.Map<List<CategoryDto>>(categories);
        return ServiceResult<List<CategoryDto>>.SuccessAsOk(categoriesDto);
    }
}

public static class GetAllCategoryEndpoint
{
    public static RouteGroupBuilder GetAllCategoryGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IMediator mediator) => (await mediator.Send(new GetAllCategoryQuery())).ToGenericResult());

        return group;
    }
}