using Application.Abstractions;
using Contracts.Services.Identity;
using Domain.Aggregates;

namespace Application.UseCases;

public class DeleteUserInteractor : Interactor<User, Command.DeleteUser>
{
    public DeleteUserInteractor(IEventStoreGateway eventStoreGateway, IEventBusGateway eventBusGateway, IUnitOfWork unitOfWork)
        : base(eventStoreGateway, eventBusGateway, unitOfWork) { }
}