namespace Application.Abstractions;

public interface ILazy<out T>
{
    T Instance { get; }
}