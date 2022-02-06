using System.Threading.Tasks;
using Application.Abstractions.Notifications;
using ECommerce.Abstractions.Validations;
using GreenPipes;
using MassTransit;
using MassTransit.Definition;
using Serilog;

namespace Infrastructure.DependencyInjection.PipeFilters;

public class BusinessValidatorFilter<T> : IFilter<ConsumeContext<T>>
    where T : class
{
    private readonly INotificationContext _notificationContext;

    public BusinessValidatorFilter(INotificationContext notificationContext)
    {
        _notificationContext = notificationContext;
    }

    public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        await next.Send(context);

        if (_notificationContext.HasErrors)
        {
            Log.Error("Business validation errors: {Errors}", _notificationContext.Errors);

            await context.Send(
                destinationAddress: new($"exchange:account.{KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(T).Name)}.business-error"),
                message: new BusinessValidationResult<T>(context.Message, _notificationContext.Errors));
        }
    }

    public void Probe(ProbeContext context)
        => context.CreateFilterScope("Business validation");
}