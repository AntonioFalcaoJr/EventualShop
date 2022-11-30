using System.ComponentModel.DataAnnotations;

namespace WebAPP.DependencyInjection.Options;

public record ECommerceHttpClientOptions
{
    [Required, Url] public string BaseAddress { get; init; } = default!;
    [Required, MinLength(5)] public string CatalogEndpoint { get; init; } = default!;
    [Required, Timestamp] public TimeSpan OverallTimeout { get; init; }
    [Required, Range(2, 20)] public int RetryCount { get; init; }
    [Required, Range(2, 20)] public int SleepDurationPower { get; init; }
    [Required, Range(2, 20)] public int EachRetryTimeout { get; init; }
    [Required, Range(2, 20)] public int CircuitBreaking { get; init; }
    [Required, Timestamp] public TimeSpan DurationOfBreak { get; init; }
}