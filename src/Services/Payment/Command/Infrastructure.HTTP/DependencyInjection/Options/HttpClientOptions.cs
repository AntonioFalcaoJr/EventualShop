using System.ComponentModel.DataAnnotations;

namespace Infrastructure.HTTP.DependencyInjection.Options;

public record CreditCardHttpClientOptions : HttpClientOptions;

public record DebitCardHttpClientOptions : HttpClientOptions;

public record PayPalHttpClientOptions : HttpClientOptions;

public abstract record HttpClientOptions
{
    [Required, Url] public required string BaseAddress { get; init; }
    [Required, MinLength(5)] public required string CancelEndpoint { get; init; }
    [Required, MinLength(5)] public required string AuthorizeEndpoint { get; init; }
    [Required, MinLength(5)] public required string RefundEndpoint { get; init; }
    [Required, MinLength(5)] public required string CaptureEndpoint { get; init; }
    [Required, Range(typeof(TimeSpan), "00:00", "23:59")] public TimeSpan OverallTimeout { get; init; }
    [Required, Range(2, 20)] public int RetryCount { get; init; }
    [Required, Range(2, 20)] public int SleepDurationPower { get; init; }
    [Required, Range(2, 20)] public int EachRetryTimeout { get; init; }
    [Required, Range(2, 20)] public int CircuitBreaking { get; init; }
    [Required, Range(typeof(TimeSpan), "00:00", "23:59")] public TimeSpan DurationOfBreak { get; init; }
}