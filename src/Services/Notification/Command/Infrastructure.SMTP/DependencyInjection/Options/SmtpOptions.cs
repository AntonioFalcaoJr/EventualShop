using System.ComponentModel.DataAnnotations;

namespace Infrastructure.SMTP.DependencyInjection.Options;

public record SmtpOptions
{
    [Required, MinLength(5)] public required string Host { get; init; }
    [Required] public int Port { get; init; }
    [Required, EmailAddress] public required string Username { get; init; }
    [Required] public required string Password { get; init; }
    [Required] public bool EnableSsl { get; init; }
}