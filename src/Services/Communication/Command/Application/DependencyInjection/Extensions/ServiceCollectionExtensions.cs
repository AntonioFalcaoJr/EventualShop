using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandInteractors(this IServiceCollection services)
        => services;
    //         .AddScoped<IInteractor<Identity.Command.ChangePassword>, ChangePasswordInteractor>();
    
    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services;
    //         .AddScoped<IInteractor<Identity.DomainEvent.UserRegistered>, ScheduleEmailConfirmationInteractor>();
}