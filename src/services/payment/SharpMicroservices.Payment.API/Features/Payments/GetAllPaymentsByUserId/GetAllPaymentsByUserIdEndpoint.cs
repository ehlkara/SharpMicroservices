using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharpMicroservices.Shared.Extensions;

namespace SharpMicroservices.Payment.API.Features.Payments.GetAllPaymentsByUserId;

public static class GetAllPaymentsByUserIdEndpoint
{
    public static RouteGroupBuilder GetAllPaymentsByUserIdGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetPaymentsByUserIdQuery(), cancellationToken);
            return result.ToGenericResult();
        })
        .WithName("get-all-payments-by-userId")
        .WithTags("Payments")
        .MapToApiVersion(1, 0)
        .Produces(StatusCodes.Status200OK)
        .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
        .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
        return group;
    }
}
