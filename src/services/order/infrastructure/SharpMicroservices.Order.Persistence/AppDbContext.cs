using Microsoft.EntityFrameworkCore;

namespace SharpMicroservices.Order.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Domain.Entities.Order> Orders => Set<Domain.Entities.Order>();
    public DbSet<Domain.Entities.OrderItem> OrderItems => Set<Domain.Entities.OrderItem>();
    public DbSet<Domain.Entities.Address> Addresses => Set<Domain.Entities.Address>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistenceAssembly).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
