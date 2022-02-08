using MassTransit;

namespace ECommerce.Abstractions.Messages.Queries.Responses;

[ExcludeFromTopology]
public interface IResponse : IMessage { }