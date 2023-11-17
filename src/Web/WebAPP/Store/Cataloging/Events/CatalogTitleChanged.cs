namespace WebAPP.Store.Cataloging.Events;

public record CatalogTitleChanged
{
    public required string CatalogId;
    public required string NewTitle;
}