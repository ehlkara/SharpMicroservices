using SharpMicroservices.Discount.Api.Repositories;

namespace SharpMicroservices.Discount.Api.Features.Discounts.GetDiscountByCode
{
    public class GetDiscountByCodeQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetDiscountByCodeQuery, ServiceResult<GetDiscountByCodeQueryResponse>>
    {
        public async Task<ServiceResult<GetDiscountByCodeQueryResponse>> Handle(GetDiscountByCodeQuery request, CancellationToken cancellationToken)
        {
            var hasDiscount = await context.Discounts.SingleOrDefaultAsync(x => x.Code == request.Code, cancellationToken);

            if (hasDiscount is null)
                return ServiceResult<GetDiscountByCodeQueryResponse>.Error("Discount not found.", $"The discount with code({request.Code}) was not found", HttpStatusCode.NotFound);

            if (hasDiscount.Expired < DateTime.Now)
                return ServiceResult<GetDiscountByCodeQueryResponse>.Error("Discount has expired.", $"The discount with code({request.Code}) has expired on {hasDiscount.Expired}.", HttpStatusCode.BadRequest);


            var response = mapper.Map<GetDiscountByCodeQueryResponse>(hasDiscount);
            return ServiceResult<GetDiscountByCodeQueryResponse>.SuccessAsOk(response);
        }
    }
}
