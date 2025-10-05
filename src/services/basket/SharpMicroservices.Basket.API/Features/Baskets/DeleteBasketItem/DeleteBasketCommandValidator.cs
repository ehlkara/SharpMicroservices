using FluentValidation;

namespace SharpMicroservices.Basket.API.Features.Baskets.DeleteBasketItem;

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketItemCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
    }
}
