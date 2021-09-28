using System;
using Messages.Abstractions.Commands;

namespace Messages.Catalogs
{
    public static class Commands
    {
        public record CreateCatalog(string Title) : Command;

        public record DeleteCatalog(Guid Id) : Command;

        public record UpdateCatalog(Guid Id, string Title) : Command;

        public record ActivateCatalog(Guid Id) : Command;

        public record DeactivateCatalog(Guid Id) : Command;

        public record RemoveCatalogItem(Guid CatalogId, Guid CatalogItemId) : Command;

        public record AddCatalogItem(Guid CatalogId, string Name, string Description, decimal Price, string PictureUri) : Command;

        public record UpdateCatalogItem(Guid CatalogId, Guid CatalogItemId, string Name, string Description, decimal Price, string PictureUri) : Command;
    }
}