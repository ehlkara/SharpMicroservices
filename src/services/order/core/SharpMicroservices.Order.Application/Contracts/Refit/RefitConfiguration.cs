using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using SharpMicroservices.Order.Application.Contracts.Refit.PaymentService;
using SharpMicroservices.Shared.Options;

namespace SharpMicroservices.Order.Application.Contracts.Refit;

public static class RefitConfiguration
{
    public static IServiceCollection AddRefitConfigurationExt(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRefitClient<IPaymentService>().ConfigureHttpClient(configure =>
        {
            var addressUrlOption = configuration.GetSection(nameof(AddressUrlOption)).Get<AddressUrlOption>();

            configure.BaseAddress = new Uri(addressUrlOption!.PaymentUrl);
        });

        return services;
    }
}
