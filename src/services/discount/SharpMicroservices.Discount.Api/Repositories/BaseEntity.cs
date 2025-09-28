using MongoDB.Bson.Serialization.Attributes;

namespace SharpMicroservices.Discount.Api.Repositories;

public class BaseEntity
{
    [BsonElement("_id")]
    public Guid Id { get; set; }
}
