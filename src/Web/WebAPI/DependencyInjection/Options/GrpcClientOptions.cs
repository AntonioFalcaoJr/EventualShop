using System.ComponentModel.DataAnnotations;

namespace WebAPI.DependencyInjection.Options;

public abstract record GrpcClientOptions
{
    [Required, Url]
    public required string BaseAddress { get; init; }
}

public record AccountGrpcClientOptions : GrpcClientOptions;
public record CatalogingCommandGrpcClientOptions : GrpcClientOptions;
public record CatalogingQueryGrpcClientOptions : GrpcClientOptions;
public record CommunicationGrpcClientOptions : GrpcClientOptions;
public record IdentityGrpcClientOptions : GrpcClientOptions;
public record PaymentGrpcClientOptions : GrpcClientOptions;
public record ShoppingCartGrpcClientOptions : GrpcClientOptions;
public record ShoppingCartCommandGrpcClientOptions : GrpcClientOptions;
public record WarehouseGrpcClientOptions : GrpcClientOptions;