namespace WebAPP.Store.Cataloging.Events;

public record CatalogDescriptionChanged
{
    public required string CatalogId;
    public required string NewDescription;
}