using MassTransit;
using SharpMicroservices.Basket.API.Features.Baskets;
using SharpMicroservices.Bus.Events;

namespace SharpMicroservices.Basket.API.Consumers;

public class OrderCreatedEventConsumer(IServiceProvider serviceProvider) : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        using var scope = serviceProvider.CreateScope();
        var basketService = scope.ServiceProvider.GetRequiredService<BasketService>();
        await basketService.DeleteBasketAsync(context.Message.UserId);
    }
}
