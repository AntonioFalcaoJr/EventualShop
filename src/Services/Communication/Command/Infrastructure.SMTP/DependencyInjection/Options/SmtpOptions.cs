using System.ComponentModel.DataAnnotations;

namespace Infrastructure.SMTP.DependencyInjection.Options;

public record SmtpOptions
{
    [Required, MinLength(5)] public string Host { get; init; }
    [Required] public int Port { get; init; }
    [Required, EmailAddress] public string Username { get; init; }
    [Required] public string Password { get; init; }
    [Required] public bool EnableSsl { get; init; }
}