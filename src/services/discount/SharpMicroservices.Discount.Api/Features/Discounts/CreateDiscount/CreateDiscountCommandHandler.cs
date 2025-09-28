using SharpMicroservices.Discount.Api.Repositories;

namespace SharpMicroservices.Discount.Api.Features.Discounts.CreateDiscount;

public class CreateDiscountCommandHandler(AppDbContext context, IMapper mapper) : IRequestHandler<CreateDiscountCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
    {
        var hasCodeForUser = await context.Discounts.AnyAsync(x => x.UserId == request.UserId && x.Code == request.Code, cancellationToken);

        if (hasCodeForUser)
            return ServiceResult.Error("The same code cannot be used again for the same user.", HttpStatusCode.BadRequest);


        var createDiscount = mapper.Map<Discount>(request);
        createDiscount.Id = NewId.NextSequentialGuid();
        createDiscount.Created = DateTime.UtcNow;

        await context.Discounts.AddAsync(createDiscount, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}
