using System;

namespace Application.Abstractions.EventSourcing.Projections
{
    public interface IProjection
    {
        public Guid Id { get; }
    }
}