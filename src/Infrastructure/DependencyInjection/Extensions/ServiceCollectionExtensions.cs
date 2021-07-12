using Infrastructure.Contexts;
using Infrastructure.DependencyInjection.Options;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventStoreDbContext(this IServiceCollection services)
            => services
                .AddScoped<DbContext, EventStoreDbContext>()
                .AddDbContext<EventStoreDbContext>();

        public static OptionsBuilder<SqlServerRetryingOptions> ConfigureSqlServerRetryingOptions(this IServiceCollection services, IConfigurationSection section)
            => services
                .AddOptions<SqlServerRetryingOptions>()
                .Bind(section)
                .ValidateDataAnnotations()
                .ValidateOnStart();

        public static IServiceCollection AddRepositories(this IServiceCollection services) 
            => services.AddScoped<ICustomerEventStoreRepository, CustomerEventStoreRepository>();
    }
}