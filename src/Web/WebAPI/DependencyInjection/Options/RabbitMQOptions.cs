using System.ComponentModel.DataAnnotations;

namespace WebAPI.DependencyInjection.Options;

public record RabbitMqOptions
{
    [Required, MinLength(5)]
    public string Host { get; init; }

    [Required, MinLength(5)]
    public string Username { get; init; }

    [Required, MinLength(5)]
    public string Password { get; init; }

    [Required]
    public string VirtualHost { get; init; }

    [Required]
    public ushort Port { get; init; }

    public List<string> Cluster { get; init; }

    [Required, MinLength(5)]
    public string SchedulerQueueName { get; init; }

    [Required, Range(1, 10)]
    public int RetryLimit { get; init; }

    [Required, Timestamp]
    public TimeSpan InitialInterval { get; init; }

    [Required, Timestamp]
    public TimeSpan IntervalIncrement { get; init; }
}