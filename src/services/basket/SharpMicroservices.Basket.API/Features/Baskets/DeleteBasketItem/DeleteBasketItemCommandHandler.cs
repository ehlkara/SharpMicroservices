using MediatR;
using SharpMicroservices.Shared;
using System.Net;
using System.Text.Json;

namespace SharpMicroservices.Basket.API.Features.Baskets.DeleteBasketItem;

public class DeleteBasketItemCommandHandler(BasketService basketService) : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
    {
        var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

        if (string.IsNullOrEmpty(basketAsJson))
        {
            return ServiceResult.Error("Basket not found.", HttpStatusCode.NotFound);
        }

        var currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);

        var basketItemToDelete = currentBasket!.Items.FirstOrDefault(bi => bi.Id == request.Id);
        if (basketItemToDelete is null)
        {
            return ServiceResult.Error("Basket item not found.", HttpStatusCode.NotFound);
        }
        currentBasket.Items.Remove(basketItemToDelete);

        await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}
