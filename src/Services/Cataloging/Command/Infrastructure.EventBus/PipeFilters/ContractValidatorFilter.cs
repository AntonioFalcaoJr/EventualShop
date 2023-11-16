using Contracts.Abstractions.Validations;
using FluentValidation;
using MassTransit;
using Serilog;

namespace Infrastructure.EventBus.PipeFilters;

public class ContractValidatorFilter<T>(IValidator<T>? validator = default) : IFilter<ConsumeContext<T>>
    where T : class
{
    public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        if (validator is null)
        {
            await next.Send(context);
            return;
        }

        var validationResult = await validator.ValidateAsync(context.Message, context.CancellationToken);

        if (validationResult.IsValid)
        {
            await next.Send(context);
            return;
        }

        Log.Error("Contract validation errors: {Errors}", validationResult.Errors);

        await context.Send(
            destinationAddress: new($"queue:shopping-cart.{KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(T).Name)}.contract-errors"),
            message: new ContractValidationResult<T>(context.Message, validationResult.Errors.Select(failure => failure.ErrorMessage)));
    }

    public void Probe(ProbeContext context)
        => context.CreateFilterScope("Contract validation");
}