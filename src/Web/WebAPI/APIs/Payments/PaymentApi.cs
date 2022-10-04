using Contracts.Services.Payment;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.Validations;

namespace WebAPI.APIs.Payments;

public static class PaymentApi
{
    public static RouteGroupBuilder MapPaymentApi(this RouteGroupBuilder group)
    {
        group.MapGet("/{paymentId:guid}", (IBus bus, [NotEmpty] Guid paymentId, CancellationToken ct)
            => ApplicationApi.GetProjectionAsync<Query.GetPayment, Projection.Payment>(bus, new(paymentId), ct));

        return group.WithMetadata(new TagsAttribute("Payments"));
    }
}