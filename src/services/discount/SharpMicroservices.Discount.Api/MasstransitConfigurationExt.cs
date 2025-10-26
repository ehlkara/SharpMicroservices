using SharpMicroservices.Bus;
using SharpMicroservices.Discount.Api.Consumers;

namespace SharpMicroservices.Discount.Api;

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


                cfg.ReceiveEndpoint("discount-microservice.order-created.queue", e =>
                {
                    e.ConfigureConsumer<OrderCreatedEventConsumer>(context);
                });
            });
        });

        return services;
    }
}
