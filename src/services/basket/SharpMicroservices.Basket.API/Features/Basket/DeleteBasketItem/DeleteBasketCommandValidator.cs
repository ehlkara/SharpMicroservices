using FluentValidation;

namespace SharpMicroservices.Basket.API.Features.Basket.DeleteBasketItem;

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketItemCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
    }
}
