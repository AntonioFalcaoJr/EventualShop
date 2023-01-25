using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Authentication.DependencyInjection.Options;

public record JwtOptions
{
    [Required, MinLength(5)] public required string SecretKey { get; init; }
    [Required, MinLength(5)] public required string Issuer { get; init; }
    [Required, MinLength(5)] public required string Audience { get; init; }
    [Required, Range(1, 1440)] public int ExpiryMinutes { get; init; }
}