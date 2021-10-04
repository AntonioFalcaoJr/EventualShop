using System;
using System.Threading.Tasks;
using FluentValidation;
using GreenPipes;
using MassTransit;
using MassTransit.Definition;
using Messages.Abstractions.Validations;

namespace Infrastructure.DependencyInjection.Filters
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

            if (validationResult.IsValid is false)
            {
                await context.Send(
                    new Uri($"queue:shoppingcart-{KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(T).Name)}-validation-error"),
                    new ValidationResultMessage<T>(context.Message, validationResult));

                return;
            }

            await next.Send(context);
        }

        public void Probe(ProbeContext context) { }
    }
}