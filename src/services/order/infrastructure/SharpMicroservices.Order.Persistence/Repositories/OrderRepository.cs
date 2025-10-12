using SharpMicroservices.Order.Application.Contracts.Repositories;

namespace SharpMicroservices.Order.Persistence.Repositories;

public class OrderRepository(AppDbContext context) : GenericRepository<Guid, Domain.Entities.Order>(context), IOrderRepository
{
}
