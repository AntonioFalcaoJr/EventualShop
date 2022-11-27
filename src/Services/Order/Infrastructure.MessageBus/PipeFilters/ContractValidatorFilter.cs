using Contracts.Abstractions.Validations;
using FluentValidation;
using MassTransit;
using Serilog;

namespace Infrastructure.MessageBus.PipeFilters;

public class ContractValidatorFilter<T> : IFilter<ConsumeContext<T>>
    where T : class
{
    private readonly IValidator<T>? _validator;

    public ContractValidatorFilter(IValidator<T>? validator = default)
    {
        _validator = validator;
    }

    public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        if (_validator is null)
        {
            await next.Send(context);
            return;
        }

        var validationResult = await _validator.ValidateAsync(context.Message, context.CancellationToken);

        if (validationResult.IsValid)
        {
            await next.Send(context);
            return;
        }

        Log.Error("Contract validation errors: {Errors}", validationResult.Errors);

        await context.Send(
            destinationAddress: new($"queue:order.{KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(T).Name)}.contract-errors"),
            message: new ContractValidationResult<T>(context.Message, validationResult.Errors.Select(failure => failure.ErrorMessage)));
    }

    public void Probe(ProbeContext context)
        => context.CreateFilterScope("Contract validation");
}