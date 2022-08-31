using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Authentication.DependencyInjection.Options;

public record JwtOptions
{
    [Required, MinLength(5)]
    public string SecretKey { get; set; }

    [Required, MinLength(5)]
    public string Issuer { get; set; }

    [Required, MinLength(5)]
    public string Audience { get; set; }

    [Required, Range(1, 1440)]
    public int ExpiryMinutes { get; set; }
};