using Application.Abstractions.Gateways;
using Domain.ValueObject;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public class NotificationOptionGatewayProvider
{
    private readonly IServiceProvider _serviceProvider;

    public NotificationOptionGatewayProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public INotificationOptionGateway GetGateway(INotificationOption option)
        => (INotificationOptionGateway)_serviceProvider.GetRequiredService(typeof(INotificationOptionGateway<>).MakeGenericType(option.GetType()));
}