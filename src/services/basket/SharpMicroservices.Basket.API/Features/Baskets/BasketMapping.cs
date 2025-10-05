using AutoMapper;
using SharpMicroservices.Basket.API.Dtos;

namespace SharpMicroservices.Basket.API.Features.Baskets;

public class BasketMapping : Profile
{
    public BasketMapping()
    {
        CreateMap<Data.BasketItem, BasketItemDto>().ReverseMap();
        CreateMap<Data.Basket, BasketDto>().ReverseMap();
    }
}
