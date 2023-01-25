using System.ComponentModel.DataAnnotations;

namespace WebAPI.DependencyInjection.Options;

public record WarehouseGrpcClientOptions
{
    [Required, Url] public required string BaseAddress { get; init; }
}