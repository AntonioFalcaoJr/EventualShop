using Application.Interfaces;
using Application.UseCases.Customers.Commands.DeleteCustomer;
using Application.UseCases.Customers.Commands.RegisterCustomer;
using Application.UseCases.Customers.Commands.UpdateCustomer;
using Application.UseCases.Customers.EventHandlers.CustomerDeleted;
using Application.UseCases.Customers.EventHandlers.CustomerRegistered;
using Application.UseCases.Customers.EventHandlers.CustomerUpdated;
using Infrastructure.DependencyInjection.Options;
using Infrastructure.EventSourcing.EventStore.Contexts;
using Infrastructure.EventSourcing.EventStore.Customers.Repositories;
using Infrastructure.Services.EventStore;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services)
            => services.AddMassTransit(
                    configurator =>
                    {
                        configurator.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host("192.168.100.9", 5672, "/", hostConfig =>
                            {
                                hostConfig.Username("guest");
                                hostConfig.Password("guest");
                            });

                            // cfg.ReceiveEndpoint("test", endpointConfigurator => endpointConfigurator.Consumer<EmailService>());
                            cfg.ConfigureEndpoints(context);
                        });

                        //configurator.AddBus(context => context.ConfigureEndpoints());
                    })
                .AddMassTransitHostedService();

        public static IServiceCollection AddMediator(this IServiceCollection services)
            => services.AddMediator(
                configurator =>
                {
                    configurator.AddConsumers(
                        typeof(RegisterCustomerHandler),
                        typeof(UpdateCustomerHandler),
                        typeof(DeleteCustomerHandler),
                        typeof(CustomerRegisteredEventHandler),
                        typeof(CustomerUpdatedEventHandler),
                        typeof(CustomerDeletedEventHandler));
                });

        public static IServiceCollection AddEventStoreDbContext(this IServiceCollection services)
            => services
                .AddScoped<DbContext, EventStoreDbContext>()
                .AddDbContext<EventStoreDbContext>();

        public static IServiceCollection AddRepositories(this IServiceCollection services)
            => services.AddScoped<ICustomerEventStoreRepository, CustomerEventStoreRepository>();

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
            => services.AddScoped<ICustomerEventStoreService, CustomerEventStoreService>();

        public static OptionsBuilder<SqlServerRetryingOptions> ConfigureSqlServerRetryingOptions(this IServiceCollection services, IConfigurationSection section)
            => services
                .AddOptions<SqlServerRetryingOptions>()
                .Bind(section)
                .ValidateDataAnnotations()
                .ValidateOnStart();

        public static OptionsBuilder<EventStoreOptions> ConfigureEventStoreOptions(this IServiceCollection services, IConfigurationSection section)
            => services
                .AddOptions<EventStoreOptions>()
                .Bind(section)
                .ValidateDataAnnotations()
                .ValidateOnStart();
    }
}