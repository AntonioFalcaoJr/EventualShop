using MassTransit;

namespace ECommerce.Abstractions;

[ExcludeFromTopology]
public interface ICommand : IMessage { }