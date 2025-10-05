using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using SharpMicroservices.Basket.API.Const;
using SharpMicroservices.Basket.API.Dtos;
using SharpMicroservices.Shared;
using SharpMicroservices.Shared.Services;
using System.Net;
using System.Text.Json;

namespace SharpMicroservices.Basket.API.Features.Basket.GetBasket;

public class GetBasketQueryHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>
{
    public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = String.Format(BasketConst.BasketCacheKey, identityService.GetUserId);

        var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

        if (string.IsNullOrEmpty(basketAsString))
        {
            return ServiceResult<BasketDto>.Error("Basket not found", HttpStatusCode.NotFound);
        }

        var basket = JsonSerializer.Deserialize<BasketDto>(basketAsString);
        return ServiceResult<BasketDto>.SuccessAsOk(basket!);
    }
}
