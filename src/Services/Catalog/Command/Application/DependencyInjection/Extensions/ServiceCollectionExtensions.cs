using Application.Services;
using Application.UseCases.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services
            .AddScoped<IApplicationService, ApplicationService>();

    public static IServiceCollection AddCommandInteractors(this IServiceCollection services)
        => services
            .AddScoped<IActivateCatalogInteractor, ActivateCatalogInteractor>()
            .AddScoped<IAddCatalogItemInteractor, AddCatalogItemInteractor>()
            .AddScoped<IChangeCatalogDescriptionInteractor, ChangeCatalogDescriptionInteractor>()
            .AddScoped<IChangeCatalogTitleInteractor, ChangeCatalogTitleInteractor>()
            .AddScoped<ICreateCatalogInteractor, CreateCatalogInteractor>()
            .AddScoped<IDeactivateCatalogInteractor, DeactivateCatalogInteractor>()
            .AddScoped<IDeleteCatalogInteractor, DeleteCatalogInteractor>()
            .AddScoped<IRemoveCatalogItemInteractor, RemoveCatalogItemInteractor>();
}