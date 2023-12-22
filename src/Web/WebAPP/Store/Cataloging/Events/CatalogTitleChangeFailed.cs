namespace WebAPP.Store.Cataloging.Events;

public record CatalogTitleChangeFailed
{
    public required string CatalogId;
    public required string Error;
}