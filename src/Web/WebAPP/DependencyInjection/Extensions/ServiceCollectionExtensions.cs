using CorrelationId.HttpClient;
using Microsoft.Extensions.Options;
using Polly;
using WebAPP.DependencyInjection.Options;
using WebAPP.HttpClients;
using WebAPP.HttpPolicies;

namespace WebAPP.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddECommerceHttpClient(this IServiceCollection services)
    {
        services
            .AddHttpClient<ICatalogHttpClient, CatalogHttpClient>()
            .AddCorrelationIdForwarding()
            .ConfigureHttpClient((provider, client) =>
            {
                var options = provider.GetRequiredService<IOptionsSnapshot<ECommerceHttpClientOptions>>().Value;
                client.BaseAddress = new(options.BaseAddress);
                client.Timeout = options.OverallTimeout;
            })
            .AddPolicyHandler((provider, _) =>
            {
                var options = provider.GetRequiredService<IOptionsSnapshot<ECommerceHttpClientOptions>>().Value;

                return Policy.WrapAsync(
                    HttpPolicy.GetRetryPolicyAsync(options.RetryCount, options.SleepDurationPower, options.EachRetryTimeout),
                    HttpPolicy.GetCircuitBreakerPolicyAsync(options.CircuitBreaking, options.DurationOfBreak));
            });
    }

    public static OptionsBuilder<ECommerceHttpClientOptions> ConfigureECommerceHttpClientOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<ECommerceHttpClientOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
}