using System.ComponentModel.DataAnnotations;

namespace WebAPI.DependencyInjection.Options;

public record CatalogGrpcClientOptions
{
    [Required, Url] public string BaseAddress { get; init; } = default!;
}