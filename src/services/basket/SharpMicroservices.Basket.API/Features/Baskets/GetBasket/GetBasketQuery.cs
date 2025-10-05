using SharpMicroservices.Basket.API.Dtos;
using SharpMicroservices.Shared;

namespace SharpMicroservices.Basket.API.Features.Baskets.GetBasket;

public record GetBasketQuery() : IRequestByServiceResult<BasketDto>;