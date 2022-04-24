using MassTransit;

namespace ECommerce.Abstractions;

[ExcludeFromTopology]
public interface IQuery : IMessage { }