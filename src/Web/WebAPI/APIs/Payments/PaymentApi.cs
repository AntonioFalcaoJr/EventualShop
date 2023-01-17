using Asp.Versioning.Builder;
using Contracts.Services.Payment.Protobuf;
using WebAPI.Abstractions;

namespace WebAPI.APIs.Payments;

public static class PaymentApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/payments/";

    public static IVersionedEndpointRouteBuilder MapPaymentApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapGet("/{paymentId:guid}/details", ([AsParameters] Queries.GetPaymentDetails query)
            => ApplicationApi.GetAsync<PaymentService.PaymentServiceClient, PaymentDetails>
                (query, (client, ct) => client.GetPaymentDetailsAsync(query, cancellationToken: ct)));

        group.MapGet("/{paymentId:guid}/methods", ([AsParameters] Queries.ListPaymentMethodListItem query)
            => ApplicationApi.ListAsync<PaymentService.PaymentServiceClient, PaymentDetails>
                (query, (client, ct) => client.ListPaymentMethodListItemAsync(query, cancellationToken: ct)));

        group.MapGet("/{paymentId:guid}/methods/{methodId:guid}", ([AsParameters] Queries.GetPaymentMethodDetails query)
            => ApplicationApi.GetAsync<PaymentService.PaymentServiceClient, PaymentMethodDetails>
                (query, (client, ct) => client.GetPaymentMethodDetailsAsync(query, cancellationToken: ct)));
        
        return builder;
    }

    public static IVersionedEndpointRouteBuilder MapPaymentApiV2(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(2);

        group.MapGet("/{paymentId:guid}", ([AsParameters] Queries.GetPaymentDetails query)
            => ApplicationApi.GetAsync<PaymentService.PaymentServiceClient, PaymentDetails>
                (query, (client, ct) => client.GetPaymentDetailsAsync(query, cancellationToken: ct)));

        return builder;
    }
}