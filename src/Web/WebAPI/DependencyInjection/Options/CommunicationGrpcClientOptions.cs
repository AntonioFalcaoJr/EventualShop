using System.ComponentModel.DataAnnotations;

namespace WebAPI.DependencyInjection.Options;

public record CommunicationGrpcClientOptions
{
    [Required, Url] public required string BaseAddress { get; init; }
}