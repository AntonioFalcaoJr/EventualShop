namespace WebAPP.Store.Cataloging.Events;

public record CatalogDescriptionChangeFailed
{
    public required string CatalogId;
    public required string Error;
}