using System;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Abstractions.Validations;
using FluentValidation;
using GreenPipes;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Infrastructure.DependencyInjection.Filters;

public class ContractValidatorFilter<T> : IFilter<ConsumeContext<T>>
    where T : class
{
    private readonly IValidator<T> _validator;
    private ValidationResult _validationResult = new();

    public ContractValidatorFilter(IServiceProvider serviceProvider)
    {
        _validator = serviceProvider.GetService<IValidator<T>>();
    }

    public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        if (_validator is not null)
            _validationResult = await _validator.ValidateAsync(context.Message, context.CancellationToken);

        if (_validationResult.IsValid is false)
        {
            Log.Error("Message validation errors: {Errors}", _validationResult.Errors);

            await context.Send(
                destinationAddress: new($"queue:catalog.{KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(T).Name)}.validation-errors"),
                message: new ContractValidationResult<T>(context.Message, _validationResult.Errors.Select(failure => failure.ErrorMessage)));

            return;
        }

        await next.Send(context);
    }

    public void Probe(ProbeContext context)
        => context.CreateFilterScope("Contract validation");
}