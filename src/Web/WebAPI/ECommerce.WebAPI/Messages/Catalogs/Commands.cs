using System;
using Messages.Catalogs.Commands;

namespace ECommerce.WebAPI.Messages.Catalogs
{
    public static class Commands
    {
        public record CreateCatalogCommand(string Title) : CreateCatalog;

        public record DeleteCatalogCommand(Guid Id) : DeleteCatalog;

        public record UpdateCatalogCommand(Guid Id, string Title) : UpdateCatalog;

        public record ActivateCatalogCommand(Guid Id) : ActivateCatalog;

        public record DeactivateCatalogCommand(Guid Id) : DeactivateCatalog;

        public record RemoveCatalogItemCommand(Guid CatalogId, Guid CatalogItemId) : RemoveCatalogItem;

        public record AddCatalogItemCommand(Guid CatalogId, string Name, string Description, decimal Price, string PictureUri) : AddCatalogItem;
    }
}