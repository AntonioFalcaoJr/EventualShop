using ECommerce.Abstractions.Messages.Queries.Responses;

namespace ECommerce.Contracts.Catalogs;

public static class Responses
{
    // public record Catalog(Guid Id, string Title, bool IsActive, bool IsDeleted) : Response;

    // public record CatalogItem(Guid Id, string Name, string Description, decimal Price, string PictureUri, bool IsDeleted) : Response, IProjection;

    public record Catalogs : ResponsePagedResult<Projections.Catalog>;

    public record CatalogItems : ResponsePagedResult<Projections.CatalogItem>;
}