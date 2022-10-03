namespace Application.DependencyInjection.Options;

public record SmtpOptions
{
    public string Host { get; init; }
    public int Port { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
    public bool EnableSsl { get; init; }
}