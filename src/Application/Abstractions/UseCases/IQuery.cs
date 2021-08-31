using MassTransit.Topology;

namespace Application.Abstractions.UseCases
{
    [ExcludeFromTopology]
    public interface IQuery { }
}