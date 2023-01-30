namespace Contracts.Abstractions;

public interface IProjection
{
    Guid Id { get; }
    bool IsDeleted { get; }
    long Version { get; }
}