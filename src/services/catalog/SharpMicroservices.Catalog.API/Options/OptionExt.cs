namespace SharpMicroservices.Catalog.API.Options;

public static class OptionExt
{
    public static IServiceCollection AddOptionsExt(this IServiceCollection services)
    {
        services.AddOptions<MongoOptions>().BindConfiguration(nameof(MongoOptions)).ValidateDataAnnotations().ValidateOnStart();
        return services;
    }
}
