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

namespace Infrastructure.DependencyInjection.PipeFilters;

public class ContractValidatorFilter<T> : IFilter<ConsumeContext<T>>
    where T : class
{
    private readonly IValidator<T> _validator;
    private ValidationResult _validationResult;

    public ContractValidatorFilter(IServiceProvider serviceProvider)
    {
        _validator = serviceProvider.GetService<IValidator<T>>();
    }

    public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        if (_validator is null)
        {
            await next.Send(context);
            return;
        }

        _validationResult = await _validator.ValidateAsync(context.Message, context.CancellationToken);

        if (_validationResult.IsValid)
        {
            await next.Send(context);
            return;
        }

        Log.Error("Contract validation errors: {Errors}", _validationResult.Errors);

        await context.Send(
            destinationAddress: new($"exchange:identity.{KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(T).Name)}.contract-errors"),
            message: new ContractValidationResult<T>(context.Message, _validationResult.Errors.Select(failure => failure.ErrorMessage)));
    }

    public void Probe(ProbeContext context)
        => context.CreateFilterScope("Contract validation");
}