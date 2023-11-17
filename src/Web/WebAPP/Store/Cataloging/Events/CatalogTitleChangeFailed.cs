namespace WebAPP.Store.Cataloging.Events;

public record CatalogTitleChangeFailed
{
    public string CatalogId;
    public string Error;
}