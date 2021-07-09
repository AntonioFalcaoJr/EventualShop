namespace Domain.Abstractions.Entities
{
    public interface IEntity<out TId>
    {
        TId Id { get; }
    }
}