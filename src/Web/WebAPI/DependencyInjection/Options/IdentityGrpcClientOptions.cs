using System.ComponentModel.DataAnnotations;

namespace WebAPI.DependencyInjection.Options;

public record IdentityGrpcClientOptions
{
    [Required, Url] public string BaseAddress { get; init; } = default!;
}