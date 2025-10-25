using Microsoft.EntityFrameworkCore;
using SharpMicroservices.Bus;
using SharpMicroservices.Order.API.Endpoints.Orders;
using SharpMicroservices.Order.Application;
using SharpMicroservices.Order.Application.Contracts.Repositories;
using SharpMicroservices.Order.Application.Contracts.UnitOfWork;
using SharpMicroservices.Order.Persistence;
using SharpMicroservices.Order.Persistence.Repositories;
using SharpMicroservices.Order.Persistence.UnitOfWork;
using SharpMicroservices.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServiceExt(typeof(OrderApplicationAssembly));
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddVersioningExt();

builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
builder.Services.AddMassTransitExt(builder.Configuration);

var app = builder.Build();
app.AddOrderGroupEndpointExt(app.AddVersionSetExt());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

await app.RunAsync();