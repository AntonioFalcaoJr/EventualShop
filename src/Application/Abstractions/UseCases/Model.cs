using System;

namespace Application.Abstractions.UseCases.Models
{
    public abstract record Model
    {
        public Guid Id { get; init; }
    }
}