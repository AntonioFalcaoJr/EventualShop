using Application.Abstractions;
using Application.Services;
using Application.UseCases.Commands;
using Contracts.Services.Catalog;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services
            .AddScoped<IApplicationService, ApplicationService>();

    public static IServiceCollection AddCommandInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Command.ActivateCatalog>, ActivateCatalogInteractor>()
            .AddScoped<IInteractor<Command.AddCatalogItem>, AddCatalogItemInteractor>()
            .AddScoped<IInteractor<Command.ChangeCatalogDescription>, ChangeCatalogDescriptionInteractor>()
            .AddScoped<IInteractor<Command.ChangeCatalogTitle>, ChangeCatalogTitleInteractor>()
            .AddScoped<IInteractor<Command.CreateCatalog>, CreateCatalogInteractor>()
            .AddScoped<IInteractor<Command.DeactivateCatalog>, DeactivateCatalogInteractor>()
            .AddScoped<IInteractor<Command.DeleteCatalog>, DeleteCatalogInteractor>()
            .AddScoped<IInteractor<Command.RemoveCatalogItem>, RemoveCatalogItemInteractor>();

}