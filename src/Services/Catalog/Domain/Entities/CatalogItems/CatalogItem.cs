using System;
using Domain.Abstractions.Entities;

namespace Domain.Entities.CatalogItems
{
    public class CatalogItem : Entity<Guid>
    {
        public CatalogItem(string name, string description, decimal price, string pictureUri)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            PictureUri = pictureUri;
        }

        public bool IsDeleted { get; private set; }

        public string Name { get; }

        public string Description { get; }

        public decimal Price { get; }

        public string PictureUri { get; }

        public void SetDelete(bool isDeleted)
            => IsDeleted = isDeleted;

        protected override bool Validate() 
            => OnValidate<Validator, CatalogItem>();
    }
}