using Application.Abstractions;
using Contracts.Services.Identity;

namespace Application.UseCases;

public class ChangePasswordInteractor : Interactor<Command.ChangePassword>
{
    public ChangePasswordInteractor(IEventStoreGateway eventStoreGateway, IEventBusGateway eventBusGateway, IUnitOfWork unitOfWork)
        : base(eventStoreGateway, eventBusGateway, unitOfWork) { }
}