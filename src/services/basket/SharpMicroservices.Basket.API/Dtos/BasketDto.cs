using System.Text.Json.Serialization;

namespace SharpMicroservices.Basket.API.Dtos;

public record BasketDto(List<BasketItemDto> Items)
{
    [JsonIgnore]
    public Guid UserId { get; set; }

    public BasketDto(Guid userId, List<BasketItemDto> basketItems) : this(basketItems)
    {
        UserId = userId;
    }

    public BasketDto() : this(new List<BasketItemDto>())
    {
    }
}
