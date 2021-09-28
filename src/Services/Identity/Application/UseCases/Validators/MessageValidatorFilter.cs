using System;
using System.Threading.Tasks;
using FluentValidation;
using GreenPipes;
using MassTransit;
using MassTransit.Definition;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Application.UseCases.Validators
{
    public class MessageValidatorFilter<T> : IFilter<ConsumeContext<T>>
        where T : class
    {
        private readonly IValidator<T> _validator;

        public MessageValidatorFilter(IValidator<T> validator)
        {
            _validator = validator;
        }

        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            var validationResult = await _validator.ValidateAsync(context.Message);

            if (validationResult.IsValid) 
                await next.Send(context);

            await context.Send(
                new Uri($"queue:identity-{KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(T).Name)}-validation-error"),
                new ValidationResultMessage<T>(context.Message, validationResult));
        }

        public void Probe(ProbeContext context) { }
    }

    public record ValidationResultMessage<TMessage>(TMessage Message, ValidationResult ValidationResult);
}