using Contracts.Abstractions.Validations;
using FluentValidation;
using MassTransit;
using Serilog;

namespace Infrastructure.MessageBus.PipeFilters;

public class BusinessValidatorFilter<T> : IFilter<ExceptionConsumeContext<T>>
    where T : class
{
    public async Task Send(ExceptionConsumeContext<T> context, IPipe<ExceptionConsumeContext<T>> next)
    {
        if (context.Exception is ValidationException exception)
        {
            Log.Error("Business validation errors: {Errors}", exception.Errors);

            await context.Send(
                destinationAddress: new($"queue:order.{KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(T).Name)}.business-error"),
                message: new BusinessValidationResult<T>(context.Message, exception.Errors.Select(failure => failure.ErrorMessage)));
        }
    }

    public void Probe(ProbeContext context)
        => context.CreateFilterScope("Business validation");
}