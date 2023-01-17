using System.ComponentModel.DataAnnotations;

namespace WebAPI.DependencyInjection.Options;

public record PaymentGrpcClientOptions
{
    [Required, Url] public string BaseAddress { get; init; } = default!;
}