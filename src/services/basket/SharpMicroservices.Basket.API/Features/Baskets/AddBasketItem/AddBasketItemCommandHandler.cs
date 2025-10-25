using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using SharpMicroservices.Basket.API.Data;
using SharpMicroservices.Shared;
using SharpMicroservices.Shared.Services;
using System.Text.Json;

namespace SharpMicroservices.Basket.API.Features.Baskets.AddBasketItem;

public class AddBasketItemCommandHandler(IIdentityService identityService, BasketService basketService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
    {
        //Fast fail
        var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

        Data.Basket? currentBasket;

        var newBasketItem = new BasketItem(request.CourseId, request.CourseName, request.CourseImageUrl, request.CoursePrice, null);

        if (string.IsNullOrEmpty(basketAsJson))
        {
            currentBasket = new Data.Basket(identityService.UserId, [newBasketItem]);
            await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }

        currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);

        var existingBasketItem = currentBasket!.Items.FirstOrDefault(bi => bi.Id == request.CourseId);

        if (existingBasketItem is not null)
        {
            // TODO: business rule
            currentBasket?.Items.Remove(existingBasketItem);
        }

        currentBasket?.Items.Add(newBasketItem);

        currentBasket.ApplyAvailableDiscount();

        await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}
