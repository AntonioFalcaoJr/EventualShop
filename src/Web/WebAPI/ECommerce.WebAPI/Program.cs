using System;
using System.Reflection;
using ECommerce.WebAPI.DependencyInjection.Extensions;
using ECommerce.WebAPI.DependencyInjection.Observers;
using MassTransit;
using MassTransit.Definition;
using Messages.JsonConverters;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new DateOnlyJsonConverter()))
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
        options.MapType<DateOnly>(() => new OpenApiSchema{Format = "date",Example = new OpenApiString(DateOnly.MaxValue.ToString())});
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
                return settings;
            });

            // Account
            MapQueueEndpoint<Messages.Accounts.Commands.DefineProfessionalAddress>();
            MapQueueEndpoint<Messages.Accounts.Commands.DefineResidenceAddress>();
            MapQueueEndpoint<Messages.Accounts.Commands.DeleteAccount>();
            MapQueueEndpoint<Messages.Accounts.Commands.CreateAccount>();
            MapQueueEndpoint<Messages.Accounts.Commands.UpdateProfile>();
            MapQueueEndpoint<Messages.Accounts.Queries.GetAccountDetails>();
            MapQueueEndpoint<Messages.Accounts.Queries.GetAccountsDetailsWithPagination>();

            // Catalog
            MapQueueEndpoint<Messages.Catalogs.Commands.CreateCatalog>();
            MapQueueEndpoint<Messages.Catalogs.Commands.UpdateCatalog>();
            MapQueueEndpoint<Messages.Catalogs.Commands.DeleteCatalog>();
            MapQueueEndpoint<Messages.Catalogs.Commands.ActivateCatalog>();
            MapQueueEndpoint<Messages.Catalogs.Commands.DeactivateCatalog>();
            MapQueueEndpoint<Messages.Catalogs.Commands.AddCatalogItem>();
            MapQueueEndpoint<Messages.Catalogs.Commands.RemoveCatalogItem>();
            MapQueueEndpoint<Messages.Catalogs.Commands.UpdateCatalogItem>();
            MapQueueEndpoint<Messages.Catalogs.Queries.GetCatalogItemsDetailsWithPagination>();

            //Identity
            MapQueueEndpoint<Messages.Identities.Commands.RegisterUser>();
            MapQueueEndpoint<Messages.Identities.Commands.ChangeUserPassword>();
            MapQueueEndpoint<Messages.Identities.Commands.DeleteUser>();
            MapQueueEndpoint<Messages.Identities.Queries.GetUserAuthenticationDetails>();

            //Shopping Cart
            MapQueueEndpoint<Messages.ShoppingCarts.Commands.CreateCart>();
            MapQueueEndpoint<Messages.ShoppingCarts.Commands.AddCartItem>();
            MapQueueEndpoint<Messages.ShoppingCarts.Commands.RemoveCartItem>();
            MapQueueEndpoint<Messages.ShoppingCarts.Commands.AddCreditCard>();
            MapQueueEndpoint<Messages.ShoppingCarts.Commands.AddShippingAddress>();
            MapQueueEndpoint<Messages.ShoppingCarts.Commands.ChangeBillingAddress>();
            MapQueueEndpoint<Messages.ShoppingCarts.Commands.CheckOutCart>();
            MapQueueEndpoint<Messages.ShoppingCarts.Queries.GetShoppingCart>();

            // Order
            MapQueueEndpoint<Messages.Orders.Commands.PlaceOrder>();

            // Payment
            MapQueueEndpoint<Messages.Payments.Commands.CancelPayment>();
            MapQueueEndpoint<Messages.Payments.Commands.RequestPayment>();
            MapQueueEndpoint<Messages.Payments.Queries.GetPaymentDetails>();
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
    => endpoints.MapControllers());

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