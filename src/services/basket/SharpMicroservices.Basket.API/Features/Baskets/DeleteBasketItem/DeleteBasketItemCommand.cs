using SharpMicroservices.Shared;

namespace SharpMicroservices.Basket.API.Features.Baskets.DeleteBasketItem;

public record DeleteBasketItemCommand(Guid Id) : IRequestByServiceResult;
