using System.ComponentModel.DataAnnotations;

namespace WebAPI.DependencyInjection.Options;

public record ShoppingCartGrpcClientOptions
{
    [Required, Url] public required string BaseAddress { get; init; }
}