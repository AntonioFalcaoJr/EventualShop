namespace Contracts.Abstractions;

public interface IProjection
{
    Guid Id { get; }
    bool IsDeleted { get; }
}

public interface IVersionedProjection : IProjection
{
    long Version { get; }
}