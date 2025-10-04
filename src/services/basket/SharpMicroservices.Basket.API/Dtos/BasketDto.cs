namespace SharpMicroservices.Basket.API.Dtos;

public record BasketDto(Guid userId, List<BasketItemDto> BasketItems);
