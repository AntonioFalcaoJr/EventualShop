using System.Reflection;
using System.Text.Json.Serialization;
using Ardalis.SmartEnum.SystemTextJson;
using Contracts.Enumerations;
using Contracts.JsonConverters;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.OpenApi.Any;
using Serilog;
using WebAPI.APIs.Accounts;
using WebAPI.APIs.Catalogs;
using WebAPI.APIs.Identities;
using WebAPI.APIs.Orders;
using WebAPI.APIs.Payments;
using WebAPI.APIs.ShoppingCarts;
using WebAPI.APIs.Warehouses;
using WebAPI.DependencyInjection.Extensions;
using WebAPI.DependencyInjection.Options;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider((context, provider) =>
{
    provider.ValidateScopes =
        provider.ValidateOnBuild =
            context.HostingEnvironment.IsDevelopment();
});

builder.Configuration
    .AddUserSecrets(Assembly.GetExecutingAssembly())
    .AddEnvironmentVariables();

Log.Logger = new LoggerConfiguration().ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging
    .ClearProviders()
    .AddSerilog();

builder.Host.UseSerilog();

builder.Host.ConfigureServices((context, services) =>
{
    services.AddCors(options
        => options.AddDefaultPolicy(policyBuilder
            => policyBuilder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()));

    services
        .AddFluentValidationAutoValidation()
        .AddFluentValidationClientsideAdapters()
        .AddValidatorsFromAssemblyContaining<Program>();

    // TODO - Review it!
    builder.Services.Configure<JsonOptions>(options =>
    {
        options.SerializerOptions.Converters.Add(new DateOnlyTextJsonConverter());
        options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.SerializerOptions.Converters.Add(new SmartEnumNameConverter<Gender, int>());
    });

    services
        .AddSwaggerGenNewtonsoftSupport()
        .AddFluentValidationRulesToSwagger()
        .AddEndpointsApiExplorer()
        .AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new() { Title = builder.Environment.ApplicationName, Version = "v1" });
            options.MapType<DateOnly>(() => new() { Format = "date", Example = new OpenApiString(DateOnly.MinValue.ToString()) });
            options.CustomSchemaIds(type => type.ToString().Replace("+", "."));
        });

    services.AddMessageBus();
    services.AddIdentityGrpcClient();
    services.AddAccountGrpcClient();

    services.ConfigureMessageBusOptions(
        context.Configuration.GetSection(nameof(MessageBusOptions)));

    services.ConfigureIdentityGrpcClientOptions(
        context.Configuration.GetSection(nameof(IdentityGrpcClientOptions)));
    
    services.ConfigureAccountGrpcClientOptions(
        context.Configuration.GetSection(nameof(AccountGrpcClientOptions)));

    services.ConfigureMassTransitHostOptions(
        context.Configuration.GetSection(nameof(MassTransitHostOptions)));

    services.ConfigureRabbitMqTransportOptions(
        context.Configuration.GetSection(nameof(RabbitMqTransportOptions)));

    services.AddHttpLogging(options
        => options.LoggingFields = HttpLoggingFields.All);
});

var app = builder.Build();

if (builder.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

if (builder.Environment.IsDevelopment() || builder.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.EnableTryItOutByDefault());
}

app.UseCors();
app.UseSerilogRequestLogging();
app.UseApplicationExceptionHandler();

app.MapGroup("/api/v1/accounts/").MapAccountApi();
app.MapGroup("/api/v1/catalogs/").MapCatalogApi();
app.MapGroup("/api/v1/identities/").MapIdentityApi();
app.MapGroup("/api/v1/orders/").MapOrderApi();
app.MapGroup("/api/v1/payments/").MapPaymentApi();
app.MapGroup("/api/v1/shopping-carts/").MapShoppingCartApi();
app.MapGroup("/api/v1/warehouses/").MapWarehouseApi();

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