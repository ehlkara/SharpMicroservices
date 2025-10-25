using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SharpMicroservices.Shared.Options;
using System.Security.Claims;

namespace SharpMicroservices.Shared.Extensions;

public static class AuthenticationExt
{
    public static IServiceCollection AddAuthenticationAndAuthorizationExt(this IServiceCollection services, IConfiguration configuration)
    {
        var identityOptions = configuration.GetSection(nameof(IdentityOption)).Get<IdentityOption>();

        services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.Authority = identityOptions.Address;
            options.Audience = identityOptions.Audience;
            options.RequireHttpsMetadata = false;

            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidateAudience = true,
                RoleClaimType = "roles",
                NameClaimType = "preferred_username"
            };
        }).AddJwtBearer("ClientCredentialSchema", options =>
        {
            options.Authority = identityOptions.Address;
            options.Audience = identityOptions.Audience;
            options.RequireHttpsMetadata = false;

            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidateAudience = true,
            };
        });

        services.AddAuthorization(options =>
        {
            // role göre policy tanımı yapıp servislere geçebiliriz.
            //options.AddPolicy("ClientCredential", policy =>
            //{
            //    policy.RequireRole("x");
            //});

            options.AddPolicy("ClientCredential", policy =>
            {
                policy.AuthenticationSchemes.Add("ClientCredentialSchema");
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("client_id");
            });

            options.AddPolicy("Password", policy =>
            {
                policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(ClaimTypes.Email);
            });
        });

        //sign
        //Aud => payment.api
        //Issuer => http://localhost:8080/realms/sharpTenant
        //TokenLifetime
        return services;
    }
}
