using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using SharpMicroservices.Basket.API.Const;
using SharpMicroservices.Basket.API.Dtos;
using SharpMicroservices.Shared;
using System.Text.Json;

namespace SharpMicroservices.Basket.API.Features.Basket.AddBasketItem;

public class AddBasketItemCommandHandler(IDistributedCache distributedCache) : IRequestHandler<AddBasketItemCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
    {
        //Fast fail

        Guid userId = Guid.NewGuid(); // Simulate getting user ID from context
        var cacheKey = String.Format(BasketConst.BasketCacheKey, userId);

        var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

        BasketDto? currentBasket;

        var newBasketItem = new BasketItemDto(request.CourseId, request.CourseName, request.CourseImageUrl, request.CoursePrice, null);

        if (string.IsNullOrEmpty(basketAsString))
        {
            currentBasket = new BasketDto(userId, [newBasketItem]);
            await CreateCacheAsync(currentBasket!, cacheKey, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
        currentBasket = JsonSerializer.Deserialize<BasketDto>(basketAsString);

        var existingBasketItem = currentBasket!.BasketItems.FirstOrDefault(bi => bi.Id == request.CourseId);

        if (existingBasketItem is not null)
        {
            currentBasket?.BasketItems.Remove(existingBasketItem);
        }

        currentBasket?.BasketItems.Add(newBasketItem);
        await CreateCacheAsync(currentBasket!, cacheKey, cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }

    private async Task CreateCacheAsync(BasketDto basket, string cacheKey, CancellationToken cancellationToken)
    {
        var basketAsString = JsonSerializer.Serialize(basket);
        await distributedCache.SetStringAsync(cacheKey, basketAsString, cancellationToken);
    }
}
