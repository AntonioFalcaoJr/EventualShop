using System.ComponentModel.DataAnnotations;

namespace WebAPI.DependencyInjection.Options;

public record CommunicationGrpcClientOptions
{
    [Required, Url] public string BaseAddress { get; init; } = default!;
}