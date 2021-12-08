using System;
using System.Reflection;
using System.Text.RegularExpressions;
using ECommerce.JsonConverters;
using ECommerce.WebAPI;
using ECommerce.WebAPI.DependencyInjection.Extensions;
using ECommerce.WebAPI.DependencyInjection.Observers;
using MassTransit;
using MassTransit.Definition;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
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

            bus.ConfigureJsonDeserializer(settings =>
            {
                settings.Converters.Add(new TypeNameHandlingConverter(TypeNameHandling.Objects));
                settings.Converters.Add(new DateOnlyJsonConverter());
                settings.Converters.Add(new ExpirationDateOnlyJsonConverter());
                return settings;
            });

            // Account
            MapQueueEndpoint<ECommerce.Contracts.Account.Commands.DefineProfessionalAddress>();
            MapQueueEndpoint<ECommerce.Contracts.Account.Commands.DefineResidenceAddress>();
            MapQueueEndpoint<ECommerce.Contracts.Account.Commands.DeleteAccount>();
            MapQueueEndpoint<ECommerce.Contracts.Account.Commands.CreateAccount>();
            MapQueueEndpoint<ECommerce.Contracts.Account.Commands.UpdateProfile>();
            MapQueueEndpoint<ECommerce.Contracts.Account.Queries.GetAccountDetails>();
            MapQueueEndpoint<ECommerce.Contracts.Account.Queries.GetAccountsDetailsWithPagination>();

            // Catalog
            MapQueueEndpoint<ECommerce.Contracts.Catalog.Commands.CreateCatalog>();
            MapQueueEndpoint<ECommerce.Contracts.Catalog.Commands.UpdateCatalog>();
            MapQueueEndpoint<ECommerce.Contracts.Catalog.Commands.DeleteCatalog>();
            MapQueueEndpoint<ECommerce.Contracts.Catalog.Commands.ActivateCatalog>();
            MapQueueEndpoint<ECommerce.Contracts.Catalog.Commands.DeactivateCatalog>();
            MapQueueEndpoint<ECommerce.Contracts.Catalog.Commands.AddCatalogItem>();
            MapQueueEndpoint<ECommerce.Contracts.Catalog.Commands.RemoveCatalogItem>();
            MapQueueEndpoint<ECommerce.Contracts.Catalog.Commands.UpdateCatalogItem>();
            MapQueueEndpoint<ECommerce.Contracts.Catalog.Queries.GetCatalogItemsDetailsWithPagination>();

            //Identity
            MapQueueEndpoint<ECommerce.Contracts.Identity.Commands.RegisterUser>();
            MapQueueEndpoint<ECommerce.Contracts.Identity.Commands.ChangeUserPassword>();
            MapQueueEndpoint<ECommerce.Contracts.Identity.Commands.DeleteUser>();
            MapQueueEndpoint<ECommerce.Contracts.Identity.Queries.GetUserAuthenticationDetails>();

            //Shopping Cart
            MapQueueEndpoint<ECommerce.Contracts.ShoppingCart.Commands.CreateCart>();
            MapQueueEndpoint<ECommerce.Contracts.ShoppingCart.Commands.AddCartItem>();
            MapQueueEndpoint<ECommerce.Contracts.ShoppingCart.Commands.RemoveCartItem>();
            MapQueueEndpoint<ECommerce.Contracts.ShoppingCart.Commands.AddCreditCard>();
            MapQueueEndpoint<ECommerce.Contracts.ShoppingCart.Commands.AddPayPal>();
            MapQueueEndpoint<ECommerce.Contracts.ShoppingCart.Commands.AddShippingAddress>();
            MapQueueEndpoint<ECommerce.Contracts.ShoppingCart.Commands.ChangeBillingAddress>();
            MapQueueEndpoint<ECommerce.Contracts.ShoppingCart.Commands.CheckOutCart>();
            MapQueueEndpoint<ECommerce.Contracts.ShoppingCart.Commands.UpdateCartItemQuantity>();
            MapQueueEndpoint<ECommerce.Contracts.ShoppingCart.Queries.GetShoppingCart>();

            // Order
            MapQueueEndpoint<ECommerce.Contracts.Order.Commands.PlaceOrder>();

            // Payment
            MapQueueEndpoint<ECommerce.Contracts.Payment.Commands.CancelPayment>();
            MapQueueEndpoint<ECommerce.Contracts.Payment.Commands.RequestPayment>();
            MapQueueEndpoint<ECommerce.Contracts.Payment.Queries.GetPaymentDetails>();
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