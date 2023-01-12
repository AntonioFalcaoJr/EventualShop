using Application.Abstractions;
using Contracts.Abstractions.Protobuf;
using Contracts.Services.Payment;
using Contracts.Services.Payment.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcService;

public class PaymentGrpcService : PaymentService.PaymentServiceBase
{
    private readonly IInteractor<Query.GetPayment, Projection.Payment> _getPaymentInteractor;

    public PaymentGrpcService(
        IInteractor<Query.GetPayment, Projection.Payment> getPaymentInteractor)
    {
        _getPaymentInteractor = getPaymentInteractor;
    }

    public override async Task<GetResponse> GetPayment(GetPaymentRequest request, ServerCallContext context)
    {
        var item = await _getPaymentInteractor.InteractAsync(request, context.CancellationToken);
        
        return item is null
            ? new() { NotFound = new() }
            : new() { Projection = Any.Pack((Contracts.Services.Payment.Protobuf.Payment)item) };
    }
}