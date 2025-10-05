using SharpMicroservices.Shared;

namespace SharpMicroservices.Basket.API.Features.Basket.ApplyDiscountCoupon;

public record ApplyDiscountCouponCommand(string Coupon, float DiscountRate) : IRequestByServiceResult;
