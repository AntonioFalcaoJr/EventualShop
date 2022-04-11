using Microsoft.Extensions.Options;
using Polly;
using WebAPP.DependencyInjection.HttpPolicies;
using WebAPP.DependencyInjection.Options;
using WebAPP.HttpClients;

namespace WebAPP.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddECommerceHttpClient(this IServiceCollection services)
    {
        services
            .AddHttpClient<IECommerceHttpClient, ECommerceHttpClient>()
            .ConfigureHttpClient((provider, client) =>
            {
                var options = provider.GetRequiredService<IOptionsMonitor<ECommerceHttpClientOptions>>().CurrentValue;
                client.BaseAddress = new(options.BaseAddress);
                client.Timeout = options.OverallTimeout;
            })
            .AddPolicyHandler((provider, _) =>
            {
                var options = provider.GetRequiredService<IOptionsMonitor<ECommerceHttpClientOptions>>().CurrentValue;

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