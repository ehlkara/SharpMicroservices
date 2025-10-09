namespace SharpMicroservices.Order.Domain.Entities;

public class BaseEntity<TEntity>
{
    public TEntity Id { get; set; } = default!;
}
