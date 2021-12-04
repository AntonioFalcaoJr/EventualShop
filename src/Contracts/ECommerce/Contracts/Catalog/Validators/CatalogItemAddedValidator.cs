using FluentValidation;

namespace ECommerce.Contracts.Catalog.Validators;

public class CatalogItemAddedValidator : AbstractValidator<DomainEvents.CatalogItemAdded> { }