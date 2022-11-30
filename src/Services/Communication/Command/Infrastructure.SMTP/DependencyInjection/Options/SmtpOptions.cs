using System.ComponentModel.DataAnnotations;

namespace Infrastructure.SMTP.DependencyInjection.Options;

public record SmtpOptions
{
    [Required, MinLength(5)] public string Host { get; init; } = default!;
    [Required] public int Port { get; init; }
    [Required, EmailAddress] public string Username { get; init; } = default!;
    [Required] public string Password { get; init; } = default!;
    [Required] public bool EnableSsl { get; init; }
}