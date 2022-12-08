using System.ComponentModel.DataAnnotations;

namespace WebAPI.Extensions.Options;

public record AccountGrpcClientOptions
{
    [Required, Url] public string BaseAddress { get; init; } = default!;
}