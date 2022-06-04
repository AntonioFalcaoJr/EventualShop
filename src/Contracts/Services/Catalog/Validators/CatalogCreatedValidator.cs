using FluentValidation;

namespace Contracts.Services.Catalog.Validators;

public class CatalogCreatedValidator : AbstractValidator<DomainEvent.CatalogCreated> { }