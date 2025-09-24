using Microsoft.Extensions.Options;

namespace SharpMicroservices.Catalog.API.Options;

public static class OptionExt
{
    public static IServiceCollection AddOptionsExt(this IServiceCollection services)
    {
        services.AddOptions<MongoOptions>().BindConfiguration(nameof(MongoOptions)).ValidateDataAnnotations().ValidateOnStart();

        services.AddSingleton<MongoOptions>(sp => sp.GetRequiredService<IOptions<MongoOptions>>().Value);

        return services;
    }
}
