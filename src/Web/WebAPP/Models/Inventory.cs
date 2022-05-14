namespace WebAPP.Models;

public class Inventory
{
    public Guid OwnerId { get; set; }
    public List<Product> Products { get; set; }
}