using System.ComponentModel.DataAnnotations;

namespace WebAPI.DependencyInjection.Options;

public record WarehouseGrpcClientOptions
{
    [Required, Url] public string BaseAddress { get; init; } = default!;
}