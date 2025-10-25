using Microsoft.Extensions.Caching.Distributed;
using SharpMicroservices.Basket.API.Const;
using SharpMicroservices.Shared.Services;
using System.Text.Json;

namespace SharpMicroservices.Basket.API.Features.Baskets;

public class BasketService(IIdentityService identityService, IDistributedCache distributedCache)
{
    private string GetCacheKey() => String.Format(BasketConst.BasketCacheKey, identityService.UserId);

    public async Task<string?> GetBasketFromCache(CancellationToken cancellationToken)
    {
        return await distributedCache.GetStringAsync(GetCacheKey(), cancellationToken);
    }

    public async Task CreateBasketCacheAsync(Data.Basket basket, CancellationToken cancellationToken)
    {
        var basketAsString = JsonSerializer.Serialize(basket);
        await distributedCache.SetStringAsync(GetCacheKey(), basketAsString, cancellationToken);
    }
}
