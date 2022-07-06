using System.Reflection;
using Contracts.Abstractions.Messages;
using Contracts.JsonConverters;
using FluentValidation.AspNetCore;
using MassTransit;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.OpenApi.Any;
using Serilog;
using WebAPI;
using WebAPI.DependencyInjection.Extensions;
using WebAPI.DependencyInjection.Options;
using WebAPI.Extensions;
using WebAPI.ParameterTransformers;

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

builder.Services.AddCors(options
    => options.AddDefaultPolicy(policyBuilder
        => policyBuilder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()));

builder.Services
    .AddRouting(options => options.LowercaseUrls = true)
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
    .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining(typeof(IMessage)));

builder.Services
    .AddSwaggerGenNewtonsoftSupport()
    .AddFluentValidationRulesToSwagger()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new() {Title = builder.Environment.ApplicationName, Version = "v1"});
        options.MapType<DateOnly>(() => new() {Format = "date", Example = new OpenApiString(DateOnly.MinValue.ToString())});
        options.CustomSchemaIds(type => type.ToString().Replace("+", "."));
    });

builder.Services.AddMessageBus();

builder.Services.ConfigureMessageBusOptions(
    builder.Configuration.GetSection(nameof(MessageBusOptions)));

builder.Services.ConfigureMassTransitHostOptions(
    builder.Configuration.GetSection(nameof(MassTransitHostOptions)));

builder.Services.ConfigureRabbitMqTransportOptions(
    builder.Configuration.GetSection(nameof(RabbitMqTransportOptions)));

builder.Services.AddHttpLogging(options
    => options.LoggingFields = HttpLoggingFields.All);

var app = builder.Build();

if (builder.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

if (builder.Environment.IsDevelopment() || builder.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.EnableTryItOutByDefault());
}

app.UseCors();

// TODO - It should be removed when migration to Minimal API completed 
app.UseRouting();
app.UseEndpoints(endpoints
    => endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller:slugify}/{action:slugify}"));
// 

app.UseSerilogRequestLogging();

app.UseApplicationExceptionHandler();

app.MapGroup("/api/v1/accounts/").MapAccountApi();

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