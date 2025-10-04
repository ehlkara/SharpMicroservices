using FluentValidation;

namespace SharpMicroservices.Basket.API.Features.Basket.DeleteBasketItem;

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketItemCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId is required.");
    }
}
