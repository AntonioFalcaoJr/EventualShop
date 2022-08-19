using System.Reflection;
using FluentValidation;

namespace WebAPI.Validations;

public class ValidationEndpointFilter : IEndpointFilter
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationEndpointFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var genericMethod = GetType()
            .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
            .FirstOrDefault(x => x.Name == nameof(ValidateAsync));
        
        foreach (var argument in context.Arguments)
        {
            var response = genericMethod.MakeGenericMethod(argument.GetType()).Invoke(this, new[] { argument });
            if (response is Task task) await task;
        }
        
        return await next(context);
    }

    private async Task ValidateAsync<T>(T argument)
    {
        var validator = _serviceProvider.GetService<IValidator<T>>();
        if (validator is null) return;

        await validator.ValidateAndThrowAsync(argument);
    }
}