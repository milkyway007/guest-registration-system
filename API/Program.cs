using Persistence;
using Microsoft.EntityFrameworkCore;
using API.Extensions;
using API.Middleware;
using FluentValidation.AspNetCore;
using Application.Events;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Application.Interfaces.Core;
using Application.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.ConfigureServices(services =>
{
    services.TryAddSingleton<IEntityFrameworkQueryableExtensionsAbstraction, EntityFrameworkQueryableExtensionsAbstraction>();
    services.TryAddSingleton<IQueryableExtensionsAbstraction, QueryableExtensionsAbstraction>();
});

builder.Services.AddControllers().AddFluentValidation(config =>
            config.RegisterValidatorsFromAssemblyContaining<Edit>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApplicationServices();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedDataAsync(context);
} catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

await app.RunAsync();
