using System.ComponentModel.DataAnnotations;

namespace WebAPI.DependencyInjection.Options;

public record ShoppingCartGrpcClientOptions
{
    [Required, Url] public string BaseAddress { get; init; } = default!;
}