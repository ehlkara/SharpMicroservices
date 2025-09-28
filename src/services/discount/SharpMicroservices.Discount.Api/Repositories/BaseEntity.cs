using MongoDB.Bson.Serialization.Attributes;

namespace SharpMicroservices.Discount.Api.Repositories;

public class BaseEntity
{
    public Guid Id { get; set; }
}
