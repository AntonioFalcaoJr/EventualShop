using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DependencyInjection.Options
{
    public record MongoDbOptions
    {
        [Required, MinLength(5)]
        public string ConnectionString { get; init; }

        [Required, MinLength(5)]
        public string Database { get; init; }

        [Required, MinLength(5)]
        public string Username { get; init; }

        [Required, MinLength(5)]
        public string Password { get; init; }

        [Required, MinLength(5)]
        public string Host { get; init; }

        [Required]
        public int Port { get; init; }

        public string ConnectionStringFormed
            => string.Format(ConnectionString, Username, Password, Host, Port, Database);
    }
}