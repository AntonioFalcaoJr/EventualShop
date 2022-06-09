using Application.Abstractions.Notifications;
using Contracts.Abstractions.Validations;
using MassTransit;
using Serilog;

namespace Infrastructure.MessageBus.PipeFilters;

public class BusinessValidatorFilter<T> : IFilter<ConsumeContext<T>>
    where T : class
{
    private readonly INotificationContext _notificationContext;

    public BusinessValidatorFilter(INotificationContext notificationContext) 
        => _notificationContext = notificationContext;

    public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        await next.Send(context);

        if (_notificationContext.HasErrors)
        {
            Log.Error("Business validation errors: {Errors}", _notificationContext.Errors);

            await context.Send(
                destinationAddress: new($"queue:order.{KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(T).Name)}.business-error"),
                message: new BusinessValidationResult<T>(context.Message, _notificationContext.Errors));
        }
    }

    public void Probe(ProbeContext context)
        => context.CreateFilterScope("Business validation");
}