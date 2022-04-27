using FluentValidation;

namespace Contracts.Services.Catalogs.Validators;

public class CatalogItemAddedValidator : AbstractValidator<DomainEvent.CatalogItemAdded> { }