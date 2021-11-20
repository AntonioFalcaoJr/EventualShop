using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DependencyInjection.Options;

public record CreditCardHttpClientOptions
{
    [Required, Url]
    public string BaseAddress { get; init; }

    [Required, MinLength(5)]
    public string CancelEndpoint { get; init; }

    [Required, MinLength(5)]
    public string AuthorizeEndpoint { get; init; }

    [Required, MinLength(5)]
    public string RefundEndpoint { get; init; }
    
    [Required, MinLength(5)]
    public string CaptureEndpoint { get; init; }
}