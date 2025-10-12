using AutoMapper;
using SharpMicroservices.Order.Application.Features.Orders.CreateOrder;

namespace SharpMicroservices.Order.Application.Features.Orders;

public class OrderMapping : Profile
{
    public OrderMapping()
    {
        CreateMap<Domain.Entities.OrderItem, OrderItemDto>().ReverseMap();
    }
}
