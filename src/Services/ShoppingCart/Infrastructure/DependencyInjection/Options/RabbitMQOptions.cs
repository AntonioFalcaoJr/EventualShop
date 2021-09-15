namespace Infrastructure.DependencyInjection.Options
{
    public record RabbitMqOptions
    {
        public string Host { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
        public string VirtualHost { get; set; }
    }
}