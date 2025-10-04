using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using FluentValidation;
using SharpMicroservices.Shared.Services;
namespace SharpMicroservices.Shared.Extensions;

public static class CommonSerivceExt
{
    public static IServiceCollection AddCommonServiceExt(this IServiceCollection services, Type assembly)
    {
        services.AddHttpContextAccessor();
        services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(assembly));

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining(assembly);
        services.AddScoped<IIdentityService, IdentityServiceFake>();

        services.AddAutoMapper(cfg => { }, assembly.Assembly);

        return services;
    }
}
