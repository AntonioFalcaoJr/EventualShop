using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Authentication.DependencyInjection.Options;

public record JwtOptions
{
    [Required, MinLength(5)] public string SecretKey { get; init; } = default!;
    [Required, MinLength(5)] public string Issuer { get; init; } = default!;
    [Required, MinLength(5)] public string Audience { get; init; } = default!;
    [Required, Range(1, 1440)] public int ExpiryMinutes { get; init; }
}