using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharpMicroservices.Order.Domain.Entities;

namespace SharpMicroservices.Order.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.Id);
        builder.Property(oi => oi.Id).UseIdentityColumn();
        builder.Property(oi => oi.ProductId).IsRequired();
        builder.Property(oi => oi.ProductName).IsRequired().HasMaxLength(200);
        builder.Property(oi => oi.UnitPrice).HasColumnType("decimal(18,2)").IsRequired();
    }
}
