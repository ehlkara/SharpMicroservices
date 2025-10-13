using FluentValidation;

namespace SharpMicroservices.Payment.API.Features.Payments.Create;

public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentCommandValidator()
    {
        RuleFor(x => x.OrderCode)
            .NotEmpty().WithMessage("Order code is required.")
            .MaximumLength(50).WithMessage("Order code must not exceed 50 characters.");
        RuleFor(x => x.CardNumber)
            .NotEmpty().WithMessage("Card number is required.")
            .CreditCard().WithMessage("Invalid card number.");
        RuleFor(x => x.CardHolderName)
            .NotEmpty().WithMessage("Card holder name is required.")
            .MaximumLength(100).WithMessage("Card holder name must not exceed 100 characters.");
        RuleFor(x => x.CardExpirationDate)
            .NotEmpty().WithMessage("Card expiration date is required.")
            .Matches(@"^(0[1-9]|1[0-2])\/?([0-9]{2})$").WithMessage("Invalid expiration date format. Use MM/YY.");
        RuleFor(x => x.CardSecurityNumber)
            .NotEmpty().WithMessage("Card security number is required.")
            .Matches(@"^\d{3,4}$").WithMessage("Invalid security number. It must be 3 or 4 digits.");
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");
    }
}
