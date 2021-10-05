using FluentValidation.AspNetCore;
using Messages.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.WebAPI.DependencyInjection.Extensions
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddApplicationFluentValidation(this IMvcBuilder builder)
            => builder.AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining(typeof(IMessage)));
    }
}