using System.ComponentModel.DataAnnotations;

namespace WebAPI.Extensions.Options;

public record IdentityGrpcClientOptions
{
    [Required, Url] public string BaseAddress { get; init; } = default!;
}