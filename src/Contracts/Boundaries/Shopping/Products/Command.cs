using Contracts.Abstractions.Messages;

namespace Contracts.Boundaries.Shopping.Products;

public static class Command
{
    public record CreateProduct(string Name, decimal Price) : Message, ICommand;
}