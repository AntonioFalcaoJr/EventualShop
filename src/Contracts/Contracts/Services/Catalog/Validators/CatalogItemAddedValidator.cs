using FluentValidation;

namespace Contracts.Services.Catalog.Validators;

public class CatalogItemAddedValidator : AbstractValidator<DomainEvent.CatalogItemAdded> { }