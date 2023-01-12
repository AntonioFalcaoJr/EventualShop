using Asp.Versioning.Builder;
using Contracts.Services.Payment;
using Contracts.Services.Payment.Protobuf;
using MassTransit;
using WebAPI.Abstractions;

namespace WebAPI.APIs.Payments;

public static class PaymentApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/payments/";

    public static IVersionedEndpointRouteBuilder MapPaymentApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapGet("/{paymentId:guid}", ([AsParameters] Query.GetPayment query)
            => ApplicationApi.GetAsync<PaymentService.PaymentServiceClient, Payment>
                (query, (client, ct) => client.GetPaymentAsync(query, cancellationToken: ct)));    
        
        return builder;
    }

    public static IVersionedEndpointRouteBuilder MapPaymentApiV2(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(2);

        group.MapGet("/{paymentId:guid}", ([AsParameters] Query.GetPayment query)
            => ApplicationApi.GetAsync<PaymentService.PaymentServiceClient, Payment>
                (query, (client, ct) => client.GetPaymentAsync(query, cancellationToken: ct)));   

        return builder;
    }
}