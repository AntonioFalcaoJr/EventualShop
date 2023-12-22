namespace WebAPP.Store.Cataloging.Events;

public record ProductsSearchFailed
{
    public string Error { get; set; } = string.Empty;
}