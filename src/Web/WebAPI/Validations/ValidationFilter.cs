using System.Reflection;
using FluentValidation;

namespace WebAPI.Validations;

public class ValidationFilter 
{
    private readonly EndpointFilterInvocationContext _context;
    private readonly EndpointFilterDelegate _next;

    public ValidationFilter(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        _context = context;
        _next = next;
    }

    public async ValueTask<object> ExecuteAsync()
    {
        var argument = _context.Arguments.FirstOrDefault();
        if (argument is null) return Task.CompletedTask;

        var validateMethod = GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(x => x.Name == nameof(ValidateAsync));
        var genericMethod = validateMethod?.MakeGenericMethod(argument.GetType());
        var result = genericMethod?.Invoke(this, new[] { argument });
        if (result is Task task) await task;
 
        return await _next(_context);
    }

    private async Task ValidateAsync<T>(T argument)
    {
        var validator = _context
            .HttpContext
            .RequestServices
            .GetService<IValidator<T>>();
        
        if (validator is not null)
            await validator.ValidateAndThrowAsync(argument);
    }
}