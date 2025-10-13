using MediatR;
using SharpMicroservices.Shared.Extensions;

namespace SharpMicroservices.Payment.API.Features.Payments.Create;

public static class CreatePaymentCommandEndpoint
{
    public static RouteGroupBuilder CreatePaymentGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (CreatePaymentCommand command, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(command, cancellationToken);
            return result.ToGenericResult();
        })
        .WithName("CreatePayment")
        .WithTags("Payments")
        .MapToApiVersion(1, 0)
        .Produces(StatusCodes.Status204NoContent)
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status500InternalServerError);
        return group;
    }
}
