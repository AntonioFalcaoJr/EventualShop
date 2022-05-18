using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
using Polly.Timeout;
using Serilog;

namespace Infrastructure.HttpClients.DependencyInjection.HttpPolicies;

public static class HttpPolicy
{
    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicyAsync(int retryCount, int sleepDurationPower, int eachRetryTimeout)
        => HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<TimeoutRejectedException>()
            .WaitAndRetryAsync(
                retryCount: retryCount,
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(sleepDurationPower, retryAttempt)),
                onRetry: (response, waitingTime, retryAttempt, _)
                    => Log.Warning(@"Retrying in {WaitingTime}s. Attempt: {RetryAttempt}/{RetryCount}. Status Code: {StatusCode}. Message: {Message}",
                        waitingTime.TotalSeconds, retryAttempt, retryCount, response?.Result?.StatusCode ?? default, response?.Exception?.Message ?? "No message"))
            .WrapAsync(
                Policy.TimeoutAsync<HttpResponseMessage>(
                    seconds: eachRetryTimeout,
                    timeoutStrategy: TimeoutStrategy.Optimistic,
                    onTimeoutAsync: async (_, timeout, _) => { await Task.Yield(); Log.Warning("Timeout applied after {AwaitedTime}s", timeout); }));

    public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicyAsync(int circuitBreaking, TimeSpan durationOfBreak)
        => HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<TimeoutRejectedException>()
            .CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking: circuitBreaking,
                durationOfBreak: durationOfBreak,
                onBreak: (_, state, breakingTime, _) => Log.Warning("Circuit breaking! State: {State}. Break time: {BreakingTime}s", state, breakingTime.TotalSeconds),
                onReset: _ => Log.Warning("Circuit resetting!"),
                onHalfOpen: () => Log.Warning("Circuit transitioning to {State}", CircuitState.HalfOpen));
}