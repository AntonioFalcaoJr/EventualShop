﻿using Application.UseCases.Events;
using Contracts.Services.Account;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers;

public class AccountRebuiltConsumer : Consumer<IntegrationEvent.ProjectionRebuilt>
{
    public AccountRebuiltConsumer(AccountRebuiltInteractor interactor) 
        : base(interactor) { }
}