using Microsoft.Extensions.DependencyInjection;

namespace SharpMicroservices.Shared.Extensions;

public static class CommonSerivceExt
{
    public static IServiceCollection AddCommonServiceExt(this IServiceCollection services, Type assembly)
    {
        services.AddHttpContextAccessor();
        services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(assembly));

        return services;
    }
}
