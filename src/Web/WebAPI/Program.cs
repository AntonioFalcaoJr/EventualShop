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
using Serilog;
using WebAPI.APIs.Accounts;
using WebAPI.APIs.Catalogs;
using WebAPI.APIs.Communications;
using WebAPI.APIs.Identities;
using WebAPI.APIs.Orders;
using WebAPI.APIs.Payments;
using WebAPI.APIs.ShoppingCarts;
using WebAPI.APIs.Warehouses;
using WebAPI.DependencyInjection.Extensions;
using WebAPI.DependencyInjection.Options;

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
    services.AddProblemDetails();

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
        .AddSwagger();

    services
        .AddApiVersioning(options => options.ReportApiVersions = true)
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

    services.AddMessageBus();
    services.AddIdentityGrpcClient();
    services.AddAccountGrpcClient();
    services.AddCommunicationGrpcClient();
    services.AddCatalogGrpcClient();

    services.ConfigureMessageBusOptions(
        context.Configuration.GetSection(nameof(MessageBusOptions)));

    services.ConfigureIdentityGrpcClientOptions(
        context.Configuration.GetSection(nameof(IdentityGrpcClientOptions)));

    services.ConfigureAccountGrpcClientOptions(
        context.Configuration.GetSection(nameof(AccountGrpcClientOptions)));

    services.ConfigureCommunicationGrpcClientOptions(
        context.Configuration.GetSection(nameof(CommunicationGrpcClientOptions)));

    services.ConfigureCatalogGrpcClientOptions(
        context.Configuration.GetSection(nameof(CatalogGrpcClientOptions)));

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

app.UseCors();
app.UseSerilogRequestLogging();

app.NewVersionedApi("Accounts").MapAccountApiV1().MapAccountApiV2();
app.NewVersionedApi("Catalogs").MapCatalogApiV1().MapCatalogApiV2();
app.NewVersionedApi("Communications").MapCommunicationApiV1().MapCommunicationApiV2();
app.NewVersionedApi("Identities").MapIdentityApiV1().MapIdentityApiV2();
app.NewVersionedApi("Orders").MapOrderApiV1().MapOrderApiV2();
app.NewVersionedApi("Payments").MapPaymentApiV1().MapPaymentApiV2();
app.NewVersionedApi("ShoppingCarts").MapShoppingCartApiV1().MapShoppingCartApiV2();
app.NewVersionedApi("Warehouses").MapWarehouseApiV1().MapWarehouseApiV2();

if (builder.Environment.IsProduction() is false)
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var version in app.DescribeApiVersions().Select(version => version.GroupName))
            options.SwaggerEndpoint($"/swagger/{version}/swagger.json", version);

        options.EnableTryItOutByDefault();
    });
}

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

// TODO - Review it! Integration tests need it, at this time.
namespace WebAPI
{
    public partial class Program { }
}