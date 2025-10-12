using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharpMicroservices.Order.Application.Features.Orders.CreateOrder;
using SharpMicroservices.Shared.Extensions;
using SharpMicroservices.Shared.Filters;

namespace SharpMicroservices.Order.API.Endpoints.Orders;

public static class CreateOrderEndpoint
{
    public static RouteGroupBuilder CreateOrderGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", async ([FromBody] CreateOrderCommand command, [FromServices] IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
            .WithName("CreateOrder")
            .MapToApiVersion(1, 0)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .AddEndpointFilter<ValidationFilter<CreateOrderCommand>>();

        return group;
    }
}
