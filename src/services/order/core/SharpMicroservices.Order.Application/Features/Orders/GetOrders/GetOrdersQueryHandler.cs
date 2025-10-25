using AutoMapper;
using MediatR;
using SharpMicroservices.Order.Application.Contracts.Repositories;
using SharpMicroservices.Order.Application.Features.Orders.CreateOrder;
using SharpMicroservices.Shared;
using SharpMicroservices.Shared.Services;

namespace SharpMicroservices.Order.Application.Features.Orders.GetOrders;

public class GetOrdersQueryHandler(IIdentityService identityService, IOrderRepository orderRepository, IMapper mapper) : IRequestHandler<GetOrdersQuery, ServiceResult<List<GetOrdersResponse>>>
{
    public async Task<ServiceResult<List<GetOrdersResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetOrdersByBuyerIdAsync(identityService.UserId);


        var response = orders.Select(o => new GetOrdersResponse(o.Created, o.TotalPrice, mapper.Map<List<OrderItemDto>>(o.OrderItems))).ToList();


        return ServiceResult<List<GetOrdersResponse>>.SuccessAsOk(response);
    }
}