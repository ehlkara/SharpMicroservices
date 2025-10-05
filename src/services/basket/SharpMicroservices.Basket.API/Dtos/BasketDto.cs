using System.Text.Json.Serialization;

namespace SharpMicroservices.Basket.API.Dtos;

public record BasketDto(List<BasketItemDto> Items)
{
    [JsonIgnore]
    public bool IsAppliedDiscount => DiscountRate is > 0 && !string.IsNullOrWhiteSpace(Coupon);
    public float? DiscountRate { get; set; }
    public string? Coupon { get; set; }

    public decimal TotalPrice => Items.Sum(i => i.Price);
    public decimal? TotalPriceByApplyDiscount => !IsAppliedDiscount ? null : Items.Sum(x => x.PriceByApplyDiscountRate);


}
