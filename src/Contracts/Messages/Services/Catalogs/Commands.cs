using System;
using Messages.Abstractions.Commands;

namespace Messages.Services.Catalogs;

public static class Commands
{
    public record CreateCatalog(string Title) : Command;

    public record DeleteCatalog(Guid CatalogId) : Command;

    public record UpdateCatalog(Guid CatalogId, string Title) : Command;

    public record ActivateCatalog(Guid CatalogId) : Command;

    public record DeactivateCatalog(Guid CatalogId) : Command;

    public record RemoveCatalogItem(Guid CatalogId, Guid CatalogItemId) : Command;

    public record AddCatalogItem(Guid CatalogId, string Name, string Description, decimal Price, string PictureUri) : Command;

    public record UpdateCatalogItem(Guid CatalogId, Guid CatalogItemId, string Name, string Description, decimal Price, string PictureUri) : Command;
}