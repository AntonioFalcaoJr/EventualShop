using System.ComponentModel.DataAnnotations;

namespace WebAPI.DependencyInjection.Options;

public record IdentityGrpcClientOptions
{
    [Required, Url] public required string BaseAddress { get; init; }
}