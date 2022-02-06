using System;
using ECommerce.Abstractions.Messages.Queries.Responses;

namespace ECommerce.Contracts.Catalog;

public static class Responses
{
    public record NotFound(string Message = "Not found.") : Response;

    public record CatalogItemsDetailsPagedResult : ResponsePagedResult<CatalogItemsDetails>;

    public record CatalogItemsDetails
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public decimal Price { get; init; }
        public string PictureUri { get; init; }
    }
}