using System.ComponentModel.DataAnnotations;

namespace WebAPI.DependencyInjection.Options;

public record PaymentGrpcClientOptions
{
    [Required, Url] public required string BaseAddress { get; init; }
}