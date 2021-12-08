using ECommerce.Abstractions;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.WebAPI.DependencyInjection.Extensions;

public static class MvcBuilderExtensions
{
    public static IMvcBuilder AddApplicationFluentValidation(this IMvcBuilder builder)
        => builder.AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining(typeof(IMessage)));
}