using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using SharpMicroservices.Basket.API.Const;
using SharpMicroservices.Basket.API.Data;
using SharpMicroservices.Shared;
using SharpMicroservices.Shared.Services;
using System.Text.Json;

namespace SharpMicroservices.Basket.API.Features.Basket.AddBasketItem;

public class AddBasketItemCommandHandler(IDistributedCache distributedCache,IIdentityService identityService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
    {
        //Fast fail

        Guid userId = identityService.GetUserId;
        var cacheKey = String.Format(BasketConst.BasketCacheKey, userId);

        var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

        Data.Basket? currentBasket;

        var newBasketItem = new BasketItem(request.CourseId, request.CourseName, request.CourseImageUrl, request.CoursePrice, null);

        if (string.IsNullOrEmpty(basketAsString))
        {
            currentBasket = new Data.Basket(userId, [newBasketItem]);
            await CreateCacheAsync(currentBasket!, cacheKey, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
        currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);

        var existingBasketItem = currentBasket!.Items.FirstOrDefault(bi => bi.Id == request.CourseId);

        if (existingBasketItem is not null)
        {
            // TODO: business rule
            currentBasket?.Items.Remove(existingBasketItem);
        }

        currentBasket?.Items.Add(newBasketItem);
        await CreateCacheAsync(currentBasket!, cacheKey, cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }

    private async Task CreateCacheAsync(Data.Basket basket, string cacheKey, CancellationToken cancellationToken)
    {
        var basketAsString = JsonSerializer.Serialize(basket);
        await distributedCache.SetStringAsync(cacheKey, basketAsString, cancellationToken);
    }
}
