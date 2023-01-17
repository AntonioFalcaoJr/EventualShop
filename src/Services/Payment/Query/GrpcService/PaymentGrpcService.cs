using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Abstractions.Protobuf;
using Contracts.Services.Payment;
using Contracts.Services.Payment.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcService;

public class PaymentGrpcService : PaymentService.PaymentServiceBase
{
    private readonly IInteractor<Query.GetPaymentDetails, Projection.PaymentDetails> _getPaymentDetailsInteractor;
    private readonly IInteractor<Query.GetPaymentMethodDetails, Projection.PaymentMethodDetails> _getPaymentMethodDetailsInteractor;
    private readonly IInteractor<Query.ListPaymentMethodListItem, IPagedResult<Projection.PaymentMethodListItem>> _listPaymentMethodListItemInteractor;

    public PaymentGrpcService(
        IInteractor<Query.GetPaymentDetails, Projection.PaymentDetails> getPaymentDetailsInteractor,
        IInteractor<Query.GetPaymentMethodDetails, Projection.PaymentMethodDetails> getPaymentMethodDetailsInteractor,
        IInteractor<Query.ListPaymentMethodListItem, IPagedResult<Projection.PaymentMethodListItem>> listPaymentMethodListItemInteractor)
    {
        _getPaymentDetailsInteractor = getPaymentDetailsInteractor;
        _getPaymentMethodDetailsInteractor = getPaymentMethodDetailsInteractor;
        _listPaymentMethodListItemInteractor = listPaymentMethodListItemInteractor;
    }

    public override async Task<GetResponse> GetPaymentDetails(GetPaymentDetailsRequest request, ServerCallContext context)
    {
        var payment = await _getPaymentDetailsInteractor.InteractAsync(request, context.CancellationToken);

        return payment is null
            ? new() { NotFound = new() }
            : new() { Projection = Any.Pack((PaymentDetails)payment) };
    }

    public override async Task<GetResponse> GetPaymentMethodDetails(GetPaymentMethodDetailsRequest request, ServerCallContext context)
    {
        var method = await _getPaymentMethodDetailsInteractor.InteractAsync(request, context.CancellationToken);

        return method is null
            ? new() { NotFound = new() }
            : new() { Projection = Any.Pack((PaymentMethodDetails)method) };
    }

    public override async Task<ListResponse> ListPaymentMethodListItem(ListPaymentMethodListItemRequest request, ServerCallContext context)
    {
        var pagedResult = await _listPaymentMethodListItemInteractor.InteractAsync(request, context.CancellationToken);

        return pagedResult!.Items.Any()
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