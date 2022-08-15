using Application.Abstractions;
using Contracts.Services.Identity;

namespace Application.UseCases;

public class RegisterUserInteractor : Interactor<Command.RegisterUser>
{
    public RegisterUserInteractor(IEventStoreGateway eventStoreGateway, IEventBusGateway eventBusGateway, IUnitOfWork unitOfWork) 
        : base(eventStoreGateway, eventBusGateway, unitOfWork) { }
}