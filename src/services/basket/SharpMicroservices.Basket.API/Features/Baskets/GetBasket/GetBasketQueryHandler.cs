using AutoMapper;
using MediatR;
using SharpMicroservices.Basket.API.Dtos;
using SharpMicroservices.Shared;
using System.Net;
using System.Text.Json;

namespace SharpMicroservices.Basket.API.Features.Baskets.GetBasket;

public class GetBasketQueryHandler(BasketService basketService, IMapper mapper) : IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>
{
    public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

        if (string.IsNullOrEmpty(basketAsJson))
        {
            return ServiceResult<BasketDto>.Error("Basket not found", HttpStatusCode.NotFound);
        }

        var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);
        var basketDto = mapper.Map<BasketDto>(basket);
        return ServiceResult<BasketDto>.SuccessAsOk(basketDto);
    }
}
