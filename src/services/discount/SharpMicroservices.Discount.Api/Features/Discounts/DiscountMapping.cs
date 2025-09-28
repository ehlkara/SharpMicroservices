using SharpMicroservices.Discount.Api.Features.Discounts.CreateDiscount;

namespace SharpMicroservices.Discount.Api.Features.Discounts;

public class DiscountMapping : Profile
{
    public DiscountMapping()
    {
        CreateMap<CreateDiscountCommand, Discount>().ReverseMap();
    }
}
