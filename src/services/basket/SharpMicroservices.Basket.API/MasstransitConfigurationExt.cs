using MassTransit;
using SharpMicroservices.Basket.API.Consumers;
using SharpMicroservices.Bus;

namespace SharpMicroservices.Basket.API;

public static class MasstransitConfigurationExt
{
    public static IServiceCollection AddMassTransitExt(this IServiceCollection services, IConfiguration configuration)
    {
        var busOptions = configuration.GetSection(nameof(BusOption)).Get<BusOption>()!;

        services.AddMassTransit(configure =>
        {
            configure.AddConsumer<OrderCreatedEventConsumer>();

            configure.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                {
                    host.Username(busOptions.UserName);
                    host.Password(busOptions.Password);
                });


                cfg.ReceiveEndpoint("basket-microservice.order-created.queue", e =>
                {
                    e.ConfigureConsumer<OrderCreatedEventConsumer>(context);
                });
            });
        });

        return services;
    }
}
