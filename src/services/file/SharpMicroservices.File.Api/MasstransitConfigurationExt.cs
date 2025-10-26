using MassTransit;
using SharpMicroservices.Bus;
using SharpMicroservices.File.Api.Consumers;

namespace SharpMicroservices.File.Api;

public static class MasstransitConfigurationExt
{
    public static IServiceCollection AddMassTransitExt(this IServiceCollection services, IConfiguration configuration)
    {
        var busOptions = configuration.GetSection(nameof(BusOption)).Get<BusOption>()!;

        services.AddMassTransit(configure =>
        {
            configure.AddConsumer<UploadCoursePictureCommandConsumer>();

            configure.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                {
                    host.Username(busOptions.UserName);
                    host.Password(busOptions.Password);
                });


                cfg.ReceiveEndpoint("file-microservice.upload-course-picture-command.queue", e =>
                {
                    e.ConfigureConsumer<UploadCoursePictureCommandConsumer>(context);
                });
            });
        });

        return services;
    }
}
