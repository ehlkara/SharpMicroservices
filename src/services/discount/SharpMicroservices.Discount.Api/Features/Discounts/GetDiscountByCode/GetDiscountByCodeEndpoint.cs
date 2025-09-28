using Microsoft.AspNetCore.Mvc;

namespace SharpMicroservices.Discount.Api.Features.Discounts.GetDiscountByCode;

public static class GetDiscountByCodeEndpoint
{
    public static RouteGroupBuilder GetDiscountByCodeGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{code:length(10)}", async (IMediator mediator, string code) => (await mediator.Send(new GetDiscountByCodeQuery(code))).ToGenericResult())
            .WithName("GetDiscountByCode").MapToApiVersion(1, 0)
            .Produces<GetDiscountByCodeQueryResponse>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);

        return group;
    }
}
