using MediatR;
using SharpMicroservices.Shared.Extensions;

namespace SharpMicroservices.Basket.API.Features.Baskets.GetBasket;

public static class GetBasketQueryEndpoint
{
    public static RouteGroupBuilder GetBasketItemGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/user", async (IMediator mediator) =>
        (await mediator.Send(new GetBasketQuery())).ToGenericResult())
            .WithName("GetBasketItem")
            .MapToApiVersion(1, 0);

        return group;
    }
}
