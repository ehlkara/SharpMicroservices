using Microsoft.EntityFrameworkCore;
using SharpMicroservices.Bus;
using SharpMicroservices.Payment.API;
using SharpMicroservices.Payment.API.Features.Payments;
using SharpMicroservices.Payment.API.Repositories;
using SharpMicroservices.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServiceExt(typeof(PaymentAssembly));
builder.Services.AddVersioningExt();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("payment-in-memory-db");
});

builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
builder.Services.AddMassTransitExt(builder.Configuration);

var app = builder.Build();
app.AddPaymentGroupEndpointExt(app.AddVersionSetExt());

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