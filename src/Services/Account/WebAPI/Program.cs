using System;
using Infrastructure.DependencyInjection.Extensions;
using Infrastructure.DependencyInjection.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddLogging(loggingBuilder =>
{
    Log.Logger = new LoggerConfiguration().ReadFrom
        .Configuration(builder.Configuration)
        .CreateLogger();

    loggingBuilder.ClearProviders();
    loggingBuilder.AddSerilog();
});

builder.Services.AddSwaggerGen(options
    => options.SwaggerDoc(
        name: "v1",
        info: new()
        {
            Title = "WebAPI",
            Version = "v1"
        }));

builder.Services.AddApplicationServices();

builder.Services.AddEventStoreRepositories();
builder.Services.AddProjectionsRepositories();

builder.Services.AddEventStoreDbContext();
builder.Services.AddProjectionsDbContext();

builder.Services.AddMassTransitWithRabbitMq(options
    => builder.Configuration.Bind(nameof(RabbitMqOptions), options));

builder.Services.ConfigureEventStoreOptions(
    builder.Configuration.GetSection(nameof(EventStoreOptions)));

builder.Services.ConfigureMongoDbOptions(
    builder.Configuration.GetSection(nameof(MongoDbOptions)));

builder.Services.ConfigureSqlServerRetryingOptions(
    builder.Configuration.GetSection(nameof(SqlServerRetryingOptions)));

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options
        => options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
}

app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());

try
{
    await using var scope = app.Services.CreateAsyncScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();
    await dbContext.Database.MigrateAsync();
    await dbContext.Database.EnsureCreatedAsync();
    await app.RunAsync();
    Log.Information("Stopped cleanly");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
}
finally
{
    Log.CloseAndFlush();
}