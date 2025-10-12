using SharpMicroservices.Shared;

namespace SharpMicroservices.Order.Application.Features.Orders.GetOrders;

public record GetOrdersQuery : IRequestByServiceResult<List<GetOrdersResponse>>;