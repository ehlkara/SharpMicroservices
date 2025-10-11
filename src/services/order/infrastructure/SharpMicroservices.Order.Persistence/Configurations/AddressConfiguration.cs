using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharpMicroservices.Order.Domain.Entities;

namespace SharpMicroservices.Order.Persistence.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).UseIdentityColumn();
        builder.Property(a => a.Province).IsRequired().HasMaxLength(200);
        builder.Property(a => a.District).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Line).IsRequired().HasMaxLength(100);
        builder.Property(a => a.ZipCode).IsRequired().HasMaxLength(5);

    }
}
