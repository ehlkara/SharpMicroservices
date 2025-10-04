using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using SharpMicroservices.Basket.API.Const;
using SharpMicroservices.Basket.API.Dtos;
using SharpMicroservices.Shared;
using SharpMicroservices.Shared.Services;
using System.Net;
using System.Text.Json;

namespace SharpMicroservices.Basket.API.Features.Basket.DeleteBasketItem;

public class DeleteBasketItemCommandHandler(IDistributedCache distributedCache,IIdentityService identityService) : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
    {
        Guid userId = identityService.GetUserId;
        var cacheKey = String.Format(BasketConst.BasketCacheKey, userId);

        var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

        if (string.IsNullOrEmpty(basketAsString))
        {
            return ServiceResult.Error("Basket not found.", HttpStatusCode.NotFound);
        }

        var currentBasket = JsonSerializer.Deserialize<BasketDto>(basketAsString);

        var basketItemToDelete = currentBasket!.BasketItems.FirstOrDefault(bi => bi.Id == request.Id);
        if (basketItemToDelete is null)
        {
            return ServiceResult.Error("Basket item not found.", HttpStatusCode.NotFound);
        }
        currentBasket.BasketItems.Remove(basketItemToDelete);

        basketAsString = JsonSerializer.Serialize(currentBasket);
        await distributedCache.SetStringAsync(cacheKey, basketAsString, cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}
