using Application.Abstractions;
using Contracts.Services.Identity;
using Domain.Aggregates;

namespace Application.UseCases;

public class ChangePasswordInteractor : Interactor<User, Command.ChangePassword>
{
    public ChangePasswordInteractor(IEventStoreGateway eventStoreGateway, IEventBusGateway eventBusGateway, IUnitOfWork unitOfWork)
        : base(eventStoreGateway, eventBusGateway, unitOfWork) { }
}