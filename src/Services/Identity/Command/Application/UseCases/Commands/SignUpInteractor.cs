﻿using Application.Abstractions;
using Application.Services;
using Contracts.Services.Identity;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class SignUpInteractor : IInteractor<Command.SignUp>
{
    private readonly IApplicationService _applicationService;

    public SignUpInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Command.SignUp command, CancellationToken cancellationToken)
    {
        var aggregate = await _applicationService.LoadAggregateAsync<User>(command.UserId, cancellationToken);
        aggregate.Handle(command);
        await _applicationService.AppendEventsAsync(aggregate, cancellationToken);
    }
}