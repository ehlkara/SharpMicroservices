namespace SharpMicroservices.Discount.Api.Features.Discounts.CreateDiscount;

public class CreateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
{
    public CreateDiscountCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required.")
            .Length(10).WithMessage("Code must not exceed 10 characters.");
        RuleFor(x => x.Rate)
            .NotEmpty().WithMessage("Rate is required.")
            .Must(rate => rate > 0).WithMessage("Rate must be a positive number.");
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");
        RuleFor(x => x.Expired)
            .GreaterThan(DateTime.UtcNow).WithMessage("Expired date must be in the future.");
    }
}
