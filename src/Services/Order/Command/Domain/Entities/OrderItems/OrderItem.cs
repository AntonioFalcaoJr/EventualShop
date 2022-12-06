using Contracts.DataTransferObjects;
using Domain.Abstractions.Entities;
using Domain.ValueObjects.Products;

namespace Domain.Entities.OrderItems;

public class OrderItem : Entity<OrderItemValidator>
{
    public OrderItem(Guid id, Product product, int quantity)
    {
        Id = id;
        Product = product;
        Quantity = quantity;
    }

    public Product Product { get; }
    public int Quantity { get; }

    public static implicit operator OrderItem(Dto.OrderItem item)
        => new(item.Id, item.Product, item.Quantity);
}