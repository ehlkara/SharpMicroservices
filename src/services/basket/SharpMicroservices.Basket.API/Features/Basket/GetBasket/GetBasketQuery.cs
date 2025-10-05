using SharpMicroservices.Basket.API.Dtos;
using SharpMicroservices.Shared;

namespace SharpMicroservices.Basket.API.Features.Basket.GetBasket;

public record GetBasketQuery() : IRequestByServiceResult<BasketDto>;