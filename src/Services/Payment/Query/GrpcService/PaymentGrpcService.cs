using Application.Abstractions;
using Contracts.Abstractions.Protobuf;
using Contracts.Boundaries.Payment;
using Contracts.Services.Payment.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcService;

public class PaymentGrpcService(IInteractor<Query.GetPaymentDetails, Projection.PaymentDetails> getPaymentDetailsInteractor,
        IInteractor<Query.GetPaymentMethodDetails, Projection.PaymentMethodDetails> getPaymentMethodDetailsInteractor,
        IPagedInteractor<Query.ListPaymentMethodListItem, Projection.PaymentMethodListItem> listPaymentMethodListItemInteractor)
    : PaymentService.PaymentServiceBase
{
    public override async Task<GetResponse> GetPaymentDetails(GetPaymentDetailsRequest request, ServerCallContext context)
    {
        var payment = await getPaymentDetailsInteractor.InteractAsync(request, context.CancellationToken);

        return payment is null
            ? new() { NotFound = new() }
            : new() { Projection = Any.Pack((PaymentDetails)payment) };
    }

    public override async Task<GetResponse> GetPaymentMethodDetails(GetPaymentMethodDetailsRequest request, ServerCallContext context)
    {
        var method = await getPaymentMethodDetailsInteractor.InteractAsync(request, context.CancellationToken);

        return method is null
            ? new() { NotFound = new() }
            : new() { Projection = Any.Pack((PaymentMethodDetails)method) };
    }

    public override async Task<ListResponse> ListPaymentMethodListItem(ListPaymentMethodListItemRequest request, ServerCallContext context)
    {
        var pagedResult = await listPaymentMethodListItemInteractor.InteractAsync(request, context.CancellationToken);

        return pagedResult.Items.Any()
            ? new()
            {
                PagedResult = new()
                {
                    Projections = { pagedResult.Items.Select(item => Any.Pack((PaymentMethodListItem)item)) },
                    Page = pagedResult.Page
                }
            }
            : new() { NoContent = new() };
    }
}