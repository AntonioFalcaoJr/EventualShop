using System.ComponentModel.DataAnnotations;

namespace WebAPI.DependencyInjection.Options;

public record AccountGrpcClientOptions
{
    [Required, Url] public string BaseAddress { get; init; } = default!;
}