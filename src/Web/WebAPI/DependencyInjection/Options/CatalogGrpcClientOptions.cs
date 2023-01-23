using System.ComponentModel.DataAnnotations;

namespace WebAPI.DependencyInjection.Options;

public record CatalogGrpcClientOptions
{
    [Required, Url] public required string BaseAddress { get; init; }
}