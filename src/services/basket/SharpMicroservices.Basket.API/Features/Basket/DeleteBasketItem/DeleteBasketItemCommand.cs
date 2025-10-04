using SharpMicroservices.Shared;

namespace SharpMicroservices.Basket.API.Features.Basket.DeleteBasketItem;

public record DeleteBasketItemCommand(Guid CourseId) : IRequestByServiceResult;
