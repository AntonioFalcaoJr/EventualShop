using ECommerce.Abstractions.Validations;
using FluentValidation;
using GreenPipes;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.DependencyInjection.Filters;

public class MessageValidatorFilter<T> : IFilter<ConsumeContext<T>>
    where T : class
{
    private readonly ILogger<MessageValidatorFilter<T>> _logger;
    private readonly IValidator<T> _validator;

    public MessageValidatorFilter(ILogger<MessageValidatorFilter<T>> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _validator = serviceProvider.GetService<IValidator<T>>();
    }

    public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        var validationResult = _validator is not null
            ? await _validator.ValidateAsync(context.Message, context.CancellationToken)
            : new();

        if (validationResult.IsValid is false)
        {
            _logger.LogError("Message validation errors: {Errors}", validationResult.Errors);

            await context.Send(
                destinationAddress: new($"queue:identity-{KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(T).Name)}-validation-errors"),
                message: new ValidationResultMessage<T>(context.Message, validationResult));

            return;
        }

        await next.Send(context);
    }

    public void Probe(ProbeContext context) { }
}