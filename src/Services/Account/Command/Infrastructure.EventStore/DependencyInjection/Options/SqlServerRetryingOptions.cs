using System.ComponentModel.DataAnnotations;

namespace Infrastructure.EventStore.DependencyInjection.Options;

public record SqlServerRetryOptions
{
    [Required, Range(5, 20)] public  required int MaxRetryCount { get; init; }
    [Required, Timestamp] public required TimeSpan MaxRetryDelay { get; init; }
    public int[]? ErrorNumbersToAdd { get; init; }
}