using System;
using Domain.Abstractions.Entities;

namespace Domain.Entities.CatalogItems;

public class CatalogItem : Entity<Guid>
{
    public CatalogItem(string name, string description, decimal price, string pictureUri)
    {
        Id = Guid.NewGuid();
        SetDescription(description);
        SetName(name);
        SetPrice(price);
        SetPictureUri(pictureUri);
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public string PictureUri { get; private set; }

    public void SetDelete(bool isDeleted)
        => IsDeleted = isDeleted;

    public void SetName(string name)
        => Name = name;

    public void SetDescription(string description)
        => Description = description;

    public void SetPrice(decimal price)
        => Price = price;

    public void SetPictureUri(string pictureUri)
        => PictureUri = pictureUri;

    protected override bool Validate()
        => OnValidate<CatalogItemValidator, CatalogItem>();
}