using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharpMicroservices.Catalog.API.Features.Categories;
using SharpMicroservices.Catalog.API.Features.Categories.Create;
using SharpMicroservices.Catalog.API.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();

var app = builder.Build();

app.AddCategoryGroupEndpointExt();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.RunAsync();