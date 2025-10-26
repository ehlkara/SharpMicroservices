using SharpMicroservices.Bus.Events;
using SharpMicroservices.Discount.Api.Features.Discounts;
using SharpMicroservices.Discount.Api.Repositories;

namespace SharpMicroservices.Discount.Api.Consumers;

public class OrderCreatedEventConsumer(IServiceProvider serviceProvider) : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        using var scope = serviceProvider.CreateScope();
        var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var discount = new Features.Discounts.Discount()
        {
            Id = NewId.NextSequentialGuid(),
            Code = DiscountCodeGenerator.Generate(10),
            Created = DateTime.UtcNow,
            Rate = 0.1f,
            Expired = DateTime.UtcNow.AddMonths(1),
            UserId = context.Message.UserId
        };
        await appDbContext.Discounts.AddAsync(discount);
        await appDbContext.SaveChangesAsync();
    }
}
