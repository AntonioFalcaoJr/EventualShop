using Application.Abstractions;
using Contracts.Services.Identity;
using Domain.Aggregates;

namespace Application.UseCases;

public class RegisterUserInteractor : Interactor<User, Command.RegisterUser>
{
    public RegisterUserInteractor(IEventStoreGateway eventStoreGateway, IEventBusGateway eventBusGateway, IUnitOfWork unitOfWork)
        : base(eventStoreGateway, eventBusGateway, unitOfWork) { }
}