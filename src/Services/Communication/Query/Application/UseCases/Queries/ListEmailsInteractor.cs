using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Communication;

namespace Application.UseCases.Queries;

public class ListEmailsInteractor : IInteractor<Query.ListEmails, IPagedResult<Projection.EmailSent>>
{
    private readonly IProjectionGateway<Projection.EmailSent> _projectionGateway;

    public ListEmailsInteractor(IProjectionGateway<Projection.EmailSent> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<IPagedResult<Projection.EmailSent>> InteractAsync(Query.ListEmails query, CancellationToken cancellationToken)
        => _projectionGateway.GetAllAsync(query.Limit, query.Offset, cancellationToken);
}