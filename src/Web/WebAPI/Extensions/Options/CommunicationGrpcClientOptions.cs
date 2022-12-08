using System.ComponentModel.DataAnnotations;

namespace WebAPI.Extensions.Options;

public record CommunicationGrpcClientOptions
{
    [Required, Url] public string BaseAddress { get; init; } = default!;
}