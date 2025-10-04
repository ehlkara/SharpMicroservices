using SharpMicroservices.Shared;

namespace SharpMicroservices.Basket.API.Features.Basket.AddBasketItem;

public record AddBasketItemCommand(Guid CourseId, string CourseName, decimal CoursePrice, string? CourseImageUrl) : IRequestByServiceResult;
