using SharpMicroservices.Order.Domain.Entities;

namespace SharpMicroservices.Order.Application.Contracts.Repositories;

public interface IOrderRepository : IGenericRepository<Guid, Domain.Entities.Order>
{
    public Task<List<Domain.Entities.Order>> GetOrdersByBuyerIdAsync(Guid buyerId);

    Task SetStatus(string orderCode, Guid paymentId, OrderStatus status);

}
