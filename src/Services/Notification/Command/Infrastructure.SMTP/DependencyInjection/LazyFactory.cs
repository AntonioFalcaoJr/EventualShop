using Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.SMTP.DependencyInjection;

public class LazyFactory<T>(IServiceProvider service) : ILazy<T>
    where T : notnull
{
    private readonly Lazy<T> _lazy = new(service.GetRequiredService<T>);
    public T Instance => _lazy.Value;
}