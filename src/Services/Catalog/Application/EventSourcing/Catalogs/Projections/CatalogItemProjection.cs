namespace Application.EventSourcing.Catalogs.Projections
{
    public record CatalogItemProjection
    {
        public string Name { get; init; }

        public string Description { get; init; }

        public decimal Price { get; init; }

        public string PictureUri { get; init; }
    }
}