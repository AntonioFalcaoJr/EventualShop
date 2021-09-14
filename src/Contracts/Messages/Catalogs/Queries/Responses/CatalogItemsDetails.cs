using System;

namespace Messages.Catalogs.Queries.Responses
{
    public interface CatalogItemsDetails
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public decimal Price { get; }
        public string PictureUri { get; }
    }
}