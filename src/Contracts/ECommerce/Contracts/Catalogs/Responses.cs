using ECommerce.Abstractions.Messages.Queries.Responses;

namespace ECommerce.Contracts.Catalogs;

public static class Responses
{
    public record Catalog : Response<Projections.Catalog>;

    public record Catalogs : ResponsePagedResult<Projections.Catalog>;

    public record CatalogItems : ResponsePagedResult<Projections.CatalogItem>;
}