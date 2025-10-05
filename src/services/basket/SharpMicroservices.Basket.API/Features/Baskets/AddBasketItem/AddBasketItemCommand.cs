using SharpMicroservices.Shared;

namespace SharpMicroservices.Basket.API.Features.Baskets.AddBasketItem;

public record AddBasketItemCommand(Guid CourseId, string CourseName, decimal CoursePrice, string? CourseImageUrl) : IRequestByServiceResult;
