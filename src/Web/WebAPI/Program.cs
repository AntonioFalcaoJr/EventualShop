using System.Reflection;
using ECommerce.Abstractions;
using ECommerce.Contracts.ShoppingCarts;
using ECommerce.JsonConverters;
using FluentValidation.AspNetCore;
using MassTransit;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.OpenApi.Any;
using Serilog;
using WebAPI.DependencyInjection.Extensions;
using WebAPI.DependencyInjection.Options;
using WebAPI.DependencyInjection.ParameterTransformers;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider((context, options) =>
{
    options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
    options.ValidateOnBuild = true;
});

builder.Host.ConfigureAppConfiguration(configurationBuilder =>
{
    configurationBuilder
        .AddUserSecrets(Assembly.GetExecutingAssembly())
        .AddEnvironmentVariables();
});

builder.Host.ConfigureLogging((context, loggingBuilder) =>
{
    Log.Logger = new LoggerConfiguration().ReadFrom
        .Configuration(context.Configuration)
        .CreateLogger();

    loggingBuilder.ClearProviders();
    loggingBuilder.AddSerilog();
    builder.Host.UseSerilog();
});

builder.Services
    .AddCors(options
        => options.AddPolicy(
            name: "cors",
            configurePolicy: policyBuilder =>
            {
                policyBuilder.AllowAnyHeader();
                policyBuilder.AllowAnyMethod();
                policyBuilder.AllowAnyOrigin();
            }));

builder.Services
    .AddRouting(options
        => options.LowercaseUrls = true)
    .AddControllers(options =>
    {
        options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
        options.SuppressAsyncSuffixInActionNames = true;
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new DateOnlyJsonConverter());
        options.SerializerSettings.Converters.Add(new ExpirationDateOnlyJsonConverter());
    })
    .AddFluentValidation(cfg =>
    {
        cfg.RegisterValidatorsFromAssemblyContaining(typeof(IMessage));
        cfg.RegisterValidatorsFromAssemblyContaining(typeof(Request));
    });

builder.Services
    .AddSwaggerGenNewtonsoftSupport()
    .AddFluentValidationRulesToSwagger()
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new() {Title = builder.Environment.ApplicationName, Version = "v1"});
        options.MapType<DateOnly>(() => new() {Format = "date", Example = new OpenApiString(DateOnly.MinValue.ToString())});
        options.CustomSchemaIds(type => type.ToString());
    });

builder.Services.AddMessageBus();

builder.Services.AddAutoMapper();

builder.Services.ConfigureMessageBusOptions(
    builder.Configuration.GetSection(nameof(MessageBusOptions)));

builder.Services.ConfigureMassTransitHostOptions(
    builder.Configuration.GetSection(nameof(MassTransitHostOptions)));

builder.Services.ConfigureRabbitMqTransportOptions(
    builder.Configuration.GetSection(nameof(RabbitMqTransportOptions)));

builder.Services.AddHttpLogging(options => options.LoggingFields = HttpLoggingFields.All);

var app = builder.Build();

if (builder.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

if (builder.Environment.IsDevelopment() || builder.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
}

app.UseCors("cors");
app.UseRouting();
app.UseSerilogRequestLogging();

app.UseEndpoints(endpoints
    => endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller:slugify}/{action:slugify}"));

try
{
    await app.RunAsync();
    Log.Information("Stopped cleanly");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
    await app.StopAsync();
}
finally
{
    Log.CloseAndFlush();
    await app.DisposeAsync();
}

namespace WebAPI
{
    public partial class Program { }
}