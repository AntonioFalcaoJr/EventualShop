using Domain.Abstractions;

namespace Domain;

public static class Exceptions
{
    public class InvalidIdentifier() : DomainException<InvalidIdentifier>("Invalid identifier.");
    public class AggregateNotFound() : DomainException<AggregateNotFound>("Aggregate not found.");
    public class ProductInventoryNotEnough() : DomainException<ProductInventoryNotEnough>("Product inventory is not enough");
    public class AggregateIsDeleted() : DomainException<AggregateIsDeleted>("Aggregate is deleted.");
    public class InvalidQuantity() : DomainException<InvalidQuantity>("Invalid quantity.");
    public class VersionMustBePositive() : DomainException<VersionMustBePositive>("Version must be a positive number.");
    public class VersionFormatException() : DomainException<VersionFormatException>("Version must be a positive number.");
    public class CatalogAlreadyCreated() : DomainException<CatalogAlreadyCreated>("Catalog is already created.");
    public class CatalogItemAlreadyAssociated() : DomainException<CatalogItemAlreadyAssociated>("Catalog item already associated.");
    public class ProductAlreadyRegistered() : DomainException<ProductAlreadyRegistered>("Product already registered.");
}