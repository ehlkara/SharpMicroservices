using SharpMicroservices.Discount.Api.Repositories;

namespace SharpMicroservices.Discount.Api.Features.Discounts.CreateDiscount;

public class CreateDiscountCommandHandler(AppDbContext context, IMapper mapper) : IRequestHandler<CreateDiscountCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
    {
        var discount = new Discount()
        {
            Id = NewId.NextSequentialGuid(),
            Code = request.Code,
            Rate = request.Rate,
            UserId = request.UserId,
            Expired = request.Expired,
            Created = DateTime.UtcNow
        };

        await context.Discounts.AddAsync(discount, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}
