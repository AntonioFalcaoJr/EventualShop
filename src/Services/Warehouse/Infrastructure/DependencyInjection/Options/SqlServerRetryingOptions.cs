using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DependencyInjection.Options;

public class SqlServerRetryOptions
{
    [Required, Range(5, 20)]
    public int MaxRetryCount { get; init; }

    [Required, Range(5, 20)]
    public int MaxRetryDelay { get; init; }

    public int[] ErrorNumbersToAdd { get; init; }
}