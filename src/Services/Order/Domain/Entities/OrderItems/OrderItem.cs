using Contracts.DataTransferObjects;
using Domain.Abstractions.Entities;
using Domain.Entities.Products;

namespace Domain.Entities.OrderItems;

public class OrderItem : Entity<Guid, OrderItemValidator>
{
    public OrderItem(Guid id, Product product, int quantity)
    {
        Id = id;
        Product = product;
        Quantity = quantity;
    }

    public Product Product { get; }
    public int Quantity { get; private set; }

    // TODO, include this in PRODUCT
    public string Category { get; }
    public string Brand { get; }

    public static implicit operator OrderItem(Dto.CartItem item)
        => new(item.Id, item.Product, item.Quantity);
}