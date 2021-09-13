using System;

namespace Messages.ShoppingCarts.Commands
{
    public interface AddShoppingCartItem
    {
        public Guid CatalogItemId { get; }
        public string CatalogItemName { get; }
        public decimal UnitPrice { get; set; }
        public Guid ShoppingCartId { get; }
        public int Quantity { get; }
    }
}