using System;
using Messages.Abstractions;

namespace Messages.Catalogs
{
    public static class Commands
    {
        public record CreateCatalog(string Title) : ICommand;

        public record DeleteCatalog(Guid Id) : ICommand;

        public record UpdateCatalog(Guid Id, string Title) : ICommand;

        public record ActivateCatalog(Guid Id) : ICommand;

        public record DeactivateCatalog(Guid Id) : ICommand;

        public record RemoveCatalogItem(Guid CatalogId, Guid CatalogItemId) : ICommand;

        public record AddCatalogItem(Guid CatalogId, string Name, string Description, decimal Price, string PictureUri) : ICommand;

        public record UpdateCatalogItem(Guid CatalogId, Guid CatalogItemId, string Name, string Description, decimal Price, string PictureUri) : ICommand;
    }
}