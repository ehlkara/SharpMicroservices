using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharpMicroservices.Bus;

public static class MasstransitConfigurationExt
{
    public static IServiceCollection AddMassTransitExt(this IServiceCollection services, IConfiguration configuration)
    {
        var busOptions = (configuration.GetSection(nameof(BusOption)).Get<BusOption>())!;

        services.AddMassTransit(configure =>
        {
            configure.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                {
                    host.Username(busOptions.UserName);
                    host.Password(busOptions.Password);
                });
            });
        });

        return services;
    }
}
