using System;
using System.Reflection;
using System.Text.RegularExpressions;
using ECommerce.WebAPI;
using ECommerce.WebAPI.DependencyInjection.Extensions;
using ECommerce.WebAPI.DependencyInjection.Observers;
using MassTransit;
using MassTransit.Definition;
using Messages.JsonConverters;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

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
    .AddApplicationFluentValidation();

builder.Services.AddLogging(loggingBuilder =>
{
    Log.Logger = new LoggerConfiguration().ReadFrom
        .Configuration(builder.Configuration)
        .CreateLogger();

    loggingBuilder.ClearProviders();
    loggingBuilder.AddSerilog();
});

builder.Services
    .AddSwaggerGenNewtonsoftSupport()
    .AddFluentValidationRulesToSwagger()
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new() { Title = "WebAPI", Version = "v1" });
        options.MapType<DateOnly>(() => new OpenApiSchema { Format = "date", Example = new OpenApiString(DateOnly.MinValue.ToString()) });
    });

builder.Services
    .AddMassTransit(cfg =>
    {
        cfg.SetKebabCaseEndpointNameFormatter();

        cfg.UsingRabbitMq((_, bus) =>
        {
            bus.Host(
                host: "192.168.100.9",
                host =>
                {
                    host.Username("guest");
                    host.Password("guest");
                });

            bus.ConnectConsumeObserver(new LoggingConsumeObserver());
            bus.ConnectSendObserver(new LoggingSendObserver());

            bus.ConfigureJsonSerializer(settings =>
            {
                settings.Converters.Add(new DateOnlyJsonConverter());
                settings.Converters.Add(new ExpirationDateOnlyJsonConverter());
                return settings;
            });

            // Account
            MapQueueEndpoint<Messages.Services.Accounts.Commands.DefineProfessionalAddress>();
            MapQueueEndpoint<Messages.Services.Accounts.Commands.DefineResidenceAddress>();
            MapQueueEndpoint<Messages.Services.Accounts.Commands.DeleteAccount>();
            MapQueueEndpoint<Messages.Services.Accounts.Commands.CreateAccount>();
            MapQueueEndpoint<Messages.Services.Accounts.Commands.UpdateProfile>();
            MapQueueEndpoint<Messages.Services.Accounts.Queries.GetAccountDetails>();
            MapQueueEndpoint<Messages.Services.Accounts.Queries.GetAccountsDetailsWithPagination>();

            // Catalog
            MapQueueEndpoint<Messages.Services.Catalogs.Commands.CreateCatalog>();
            MapQueueEndpoint<Messages.Services.Catalogs.Commands.UpdateCatalog>();
            MapQueueEndpoint<Messages.Services.Catalogs.Commands.DeleteCatalog>();
            MapQueueEndpoint<Messages.Services.Catalogs.Commands.ActivateCatalog>();
            MapQueueEndpoint<Messages.Services.Catalogs.Commands.DeactivateCatalog>();
            MapQueueEndpoint<Messages.Services.Catalogs.Commands.AddCatalogItem>();
            MapQueueEndpoint<Messages.Services.Catalogs.Commands.RemoveCatalogItem>();
            MapQueueEndpoint<Messages.Services.Catalogs.Commands.UpdateCatalogItem>();
            MapQueueEndpoint<Messages.Services.Catalogs.Queries.GetCatalogItemsDetailsWithPagination>();

            //Identity
            MapQueueEndpoint<Messages.Services.Identities.Commands.RegisterUser>();
            MapQueueEndpoint<Messages.Services.Identities.Commands.ChangeUserPassword>();
            MapQueueEndpoint<Messages.Services.Identities.Commands.DeleteUser>();
            MapQueueEndpoint<Messages.Services.Identities.Queries.GetUserAuthenticationDetails>();

            //Shopping Cart
            MapQueueEndpoint<Messages.Services.ShoppingCarts.Commands.CreateCart>();
            MapQueueEndpoint<Messages.Services.ShoppingCarts.Commands.AddCartItem>();
            MapQueueEndpoint<Messages.Services.ShoppingCarts.Commands.RemoveCartItem>();
            MapQueueEndpoint<Messages.Services.ShoppingCarts.Commands.AddCreditCard>();
            MapQueueEndpoint<Messages.Services.ShoppingCarts.Commands.AddShippingAddress>();
            MapQueueEndpoint<Messages.Services.ShoppingCarts.Commands.ChangeBillingAddress>();
            MapQueueEndpoint<Messages.Services.ShoppingCarts.Commands.CheckOutCart>();
            MapQueueEndpoint<Messages.Services.ShoppingCarts.Commands.UpdateCartItemQuantity>();
            MapQueueEndpoint<Messages.Services.ShoppingCarts.Queries.GetShoppingCart>();

            // Order
            MapQueueEndpoint<Messages.Services.Orders.Commands.PlaceOrder>();

            // Payment
            MapQueueEndpoint<Messages.Services.Payments.Commands.CancelPayment>();
            MapQueueEndpoint<Messages.Services.Payments.Commands.RequestPayment>();
            MapQueueEndpoint<Messages.Services.Payments.Queries.GetPaymentDetails>();
        });
    })
    .AddGenericRequestClient()
    .AddMassTransitHostedService();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce.WebAPI v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

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
}
finally
{
    Log.CloseAndFlush();
}

static void MapQueueEndpoint<TMessage>()
    where TMessage : class
    => EndpointConvention.Map<TMessage>(new Uri($"exchange:{ToKebabCaseString(typeof(TMessage))}"));

static string ToKebabCaseString(MemberInfo member)
    => KebabCaseEndpointNameFormatter.Instance.SanitizeName(member.Name);

namespace ECommerce.WebAPI
{
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value)
            => Regex.Replace(value.ToString() ?? string.Empty, "([a-z])([A-Z])", "$1-$2").ToLower();
    }
}