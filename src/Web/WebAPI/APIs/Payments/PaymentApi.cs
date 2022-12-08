using Asp.Versioning.Builder;
using Contracts.Services.Payment;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.Validations;

namespace WebAPI.APIs.Payments;

public static class PaymentApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/payments/";

    public static IVersionedEndpointRouteBuilder MapPaymentApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapGet("/{paymentId:guid}", (IBus bus, [NotEmpty] Guid paymentId, CancellationToken cancellationToken)
            => ApplicationApi.GetProjectionAsync<Query.GetPayment, Projection.Payment>(bus, new(paymentId), cancellationToken));

        return builder;
    }

    public static IVersionedEndpointRouteBuilder MapPaymentApiV2(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(2);

        group.MapGet("/{paymentId:guid}", (IBus bus, [NotEmpty] Guid paymentId, CancellationToken cancellationToken)
            => ApplicationApi.GetProjectionAsync<Query.GetPayment, Projection.Payment>(bus, new(paymentId), cancellationToken));

        return builder;
    }
}