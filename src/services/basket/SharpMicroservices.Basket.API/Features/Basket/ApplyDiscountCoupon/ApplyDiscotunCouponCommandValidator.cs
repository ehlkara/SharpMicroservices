using FluentValidation;

namespace SharpMicroservices.Basket.API.Features.Basket.ApplyDiscountCoupon;

public class ApplyDiscotunCouponCommandValidator : AbstractValidator<ApplyDiscountCouponCommand>
{
    public ApplyDiscotunCouponCommandValidator()
    {
        RuleFor(x => x.Coupon).NotEmpty().WithMessage("{PropertyName} is required.");
        RuleFor(x => x.DiscountRate).GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
    }
}
