namespace WebAPP.Models;

public class CatalogItem
{
    public Guid Id { get; set; }
    public Guid CatalogId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }

}