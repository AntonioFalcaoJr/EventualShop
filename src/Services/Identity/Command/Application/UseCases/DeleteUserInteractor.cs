using Application.Abstractions;
using Contracts.Services.Identity;

namespace Application.UseCases;

public class DeleteUserInteractor : Interactor<Command.DeleteUser>
{
    public DeleteUserInteractor(IEventStoreGateway eventStoreGateway, IEventBusGateway eventBusGateway, IUnitOfWork unitOfWork) 
        : base(eventStoreGateway, eventBusGateway, unitOfWork) { }
}