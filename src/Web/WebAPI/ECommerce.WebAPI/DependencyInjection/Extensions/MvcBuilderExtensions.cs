using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Messages.Abstractions;

namespace ECommerce.WebAPI.DependencyInjection.Extensions
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddApplicationFluentValidation(this IMvcBuilder builder)
            => builder.AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining(typeof(IMessage)));
    }
}