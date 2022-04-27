using FluentValidation;

namespace Contracts.Services.Catalogs.Validators;

public class CatalogCreatedValidator : AbstractValidator<DomainEvent.CatalogCreated> { }