using ECommerce.Abstractions.Queries.Responses;

namespace ECommerce.Contracts.Catalog;

public static class Responses
{
    public record CatalogItemsDetails
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public decimal Price { get; init; }
        public string PictureUri { get; init; }
    }

    public record CatalogItemsDetailsPagedResult : ResponsePagedResult<CatalogItemsDetails>;
}