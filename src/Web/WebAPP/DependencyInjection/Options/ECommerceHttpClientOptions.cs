using System.ComponentModel.DataAnnotations;

namespace WebAPP.DependencyInjection.Options;

public record ECommerceHttpClientOptions
{
    [Required, Url] public required string BaseAddress { get; init; }
    [Required, MinLength(5)] public required string CatalogEndpoint { get; init; }
    [Required, Range(typeof(TimeSpan), "00:00", "23:59")] public TimeSpan OverallTimeout { get; init; }
    [Required, Range(2, 20)] public int RetryCount { get; init; }
    [Required, Range(2, 20)] public int SleepDurationPower { get; init; }
    [Required, Range(2, 20)] public int EachRetryTimeout { get; init; }
    [Required, Range(2, 20)] public int CircuitBreaking { get; init; }
    [Required, Range(typeof(TimeSpan), "00:00", "23:59")] public TimeSpan DurationOfBreak { get; init; }
}