using SharpMicroservices.Bus;
using SharpMicroservices.Catalog.API.Consumers;

namespace SharpMicroservices.Catalog.API;

public static class MasstransitConfigurationExt
{
    public static IServiceCollection AddMassTransitExt(this IServiceCollection services, IConfiguration configuration)
    {
        var busOptions = configuration.GetSection(nameof(BusOption)).Get<BusOption>()!;

        services.AddMassTransit(configure =>
        {
            configure.AddConsumer<CoursePictureUploadedEventConsumer>();

            configure.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                {
                    host.Username(busOptions.UserName);
                    host.Password(busOptions.Password);
                });


                cfg.ReceiveEndpoint("catalog-microservice.course-picture-uploaded.queue", e =>
                {
                    e.ConfigureConsumer<CoursePictureUploadedEventConsumer>(context);
                });
            });
        });

        return services;
    }
}
