using SharpMicroservices.Basket.API;
using SharpMicroservices.Basket.API.Features.Baskets;
using SharpMicroservices.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServiceExt(typeof(BasketAssembly));
builder.Services.AddScoped<BasketService>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});
builder.Services.AddVersioningExt();

builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);

var app = builder.Build();
app.AddBasketGroupEndpointExt(app.AddVersionSetExt());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

await app.RunAsync();