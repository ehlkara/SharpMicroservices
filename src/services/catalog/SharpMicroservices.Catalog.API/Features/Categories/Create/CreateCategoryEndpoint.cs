using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SharpMicroservices.Catalog.API.Features.Categories.Create;

public static class CreateCategoryEndpoint
{
    public static RouteGroupBuilder CreateCategoryGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (CreateCategoryCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);



            return new ObjectResult(result)
            {
                StatusCode = result.Status.GetHashCode()
            };
        });

        return group;
    }
}
