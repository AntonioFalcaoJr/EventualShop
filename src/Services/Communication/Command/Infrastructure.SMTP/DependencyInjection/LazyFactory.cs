using Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.SMTP.DependencyInjection;

public class LazyFactory<T> : ILazy<T>
    where T : notnull
{
    private readonly Lazy<T> _lazy;
    public T Instance => _lazy.Value;

    public LazyFactory(IServiceProvider service)
        => _lazy = new(service.GetRequiredService<T>);
}