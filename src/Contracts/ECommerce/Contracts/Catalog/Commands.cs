﻿using System;
using ECommerce.Abstractions.Commands;

namespace ECommerce.Contracts.Catalog;

public static class Commands
{
    public record CreateCatalog(string Title) : Command;

    public record DeleteCatalog(Guid CatalogId) : Command(CatalogId);

    public record UpdateCatalog(Guid CatalogId, string Title) : Command(CatalogId);

    public record ActivateCatalog(Guid CatalogId) : Command(CatalogId);

    public record DeactivateCatalog(Guid CatalogId) : Command(CatalogId);

    public record RemoveCatalogItem(Guid CatalogId, Guid CatalogItemId) : Command(CatalogId);

    public record AddCatalogItem(Guid CatalogId, string Name, string Description, decimal Price, string PictureUri) : Command(CatalogId);

    public record UpdateCatalogItem(Guid CatalogId, Guid CatalogItemId, string Name, string Description, decimal Price, string PictureUri) : Command(CatalogId);
}