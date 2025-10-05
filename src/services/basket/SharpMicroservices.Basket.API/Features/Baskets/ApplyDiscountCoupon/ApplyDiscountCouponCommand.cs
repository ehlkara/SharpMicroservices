using SharpMicroservices.Shared;

namespace SharpMicroservices.Basket.API.Features.Baskets.ApplyDiscountCoupon;

public record ApplyDiscountCouponCommand(string Coupon, float DiscountRate) : IRequestByServiceResult;
