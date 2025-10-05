namespace SharpMicroservices.Basket.API.Data;
// Anamic model = rich domain model (behavior + data)
public class Basket
{
    public Guid UserId { get; set; }
    public List<BasketItem> Items { get; set; }
    public float? DiscountRate { get; set; }
    public string? Coupon { get; set; }
    public bool IsAppliedDiscount => DiscountRate is > 0 && !string.IsNullOrWhiteSpace(Coupon);
    public decimal TotalPrice => Items.Sum(i => i.Price);
    public decimal? TotalPriceByApplyDiscount => !IsAppliedDiscount ? null : Items.Sum(x => x.PriceByApplyDiscountRate);

    public Basket()
    {
    }

    public Basket(Guid userId, List<BasketItem> items)
    {
        UserId = userId;
        Items = items;
    }

    public void ApplyNewDiscount(float discountRate, string coupon)
    {
        DiscountRate = discountRate;
        Coupon = coupon;
        foreach (var basket in Items)
        {
            basket.PriceByApplyDiscountRate = basket.Price * (decimal)(1 - discountRate);
        }
    }
    public void ApplyAvailableDiscount()
    {
        foreach (var basket in Items)
        {
            basket.PriceByApplyDiscountRate = basket.Price * (decimal)(1 - DiscountRate!);
        }
    }

    public void ClearDiscount()
    {
        DiscountRate = null;
        Coupon = null;
        foreach (var basket in Items)
        {
            basket.PriceByApplyDiscountRate = null;
        }
    }

}
