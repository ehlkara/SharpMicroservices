using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMicroservices.Order.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Domain.Entities.Order>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Entities.Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).ValueGeneratedNever();
        builder.Property(o => o.BuyerId).IsRequired();
        builder.Property(o => o.Created).IsRequired();
        builder.Property(o => o.Status).IsRequired();
        builder.Property(o => o.Code).IsRequired().HasMaxLength(10);
        builder.Property(o => o.AddressId).IsRequired();
        builder.Property(o => o.TotalPrice).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(o => o.DiscountRate).HasColumnType("float");

        builder.HasMany(o => o.OrderItems).WithOne(x => x.Order).HasForeignKey(x => x.OrderId);
        builder.HasOne(o => o.Address).WithMany().HasForeignKey(o => o.AddressId);
    }
}
