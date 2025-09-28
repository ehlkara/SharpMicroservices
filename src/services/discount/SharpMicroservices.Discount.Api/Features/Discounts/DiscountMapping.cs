using SharpMicroservices.Discount.Api.Features.Discounts.CreateDiscount;
using SharpMicroservices.Discount.Api.Features.Discounts.GetDiscountByCode;

namespace SharpMicroservices.Discount.Api.Features.Discounts;

public class DiscountMapping : Profile
{
    public DiscountMapping()
    {
        CreateMap<CreateDiscountCommand, Discount>().ReverseMap();
        CreateMap<Discount, GetDiscountByCodeQueryResponse>()
            .ForMember(x => x.Code, c => c.MapFrom(c => c.Code))
            .ForMember(x => x.Rate, c => c.MapFrom(c => c.Rate))
            .ReverseMap();
    }
}
