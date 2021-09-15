using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DependencyInjection.Options
{
    public record RabbitMqOptions
    {
        [Required, MinLength(5)]
        public string Host { get; init; }

        [Required, MinLength(5)]
        public string Username { get; init; }

        [Required, MinLength(5)]
        public string Password { get; init; }

        [Required, MinLength(5)]
        public string VirtualHost { get; set; }
    }
}