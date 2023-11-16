using System.ComponentModel.DataAnnotations;

namespace Infrastructure.EventBus.DependencyInjection.Options;

public record EventBusOptions
{
    [Required] public required string ConnectionName { get; init; }
    [Required] public required Uri ConnectionString { get; init; }
    [Required, Range(1, 10)] public int RetryLimit { get; init; }
    [Required, Timestamp] public TimeSpan InitialInterval { get; init; }
    [Required, Timestamp] public TimeSpan IntervalIncrement { get; init; }
    [Required, MinLength(5)] public required string SchedulerQueueName { get; init; }
}