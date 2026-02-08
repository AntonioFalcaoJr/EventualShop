using Contracts.Abstractions;
using Contracts.Abstractions.Paging;
using MediatR;

namespace Application.UseCases.Queries;

public record SearchProductsListItemQuery(string Fragment, Paging Paging) : IRequest<IEnumerable<IProjection>>;

public class SearchProductsListItemInteractor(ISearchGateway gateway) : IRequestHandler<SearchProductsListItemQuery, IEnumerable<IProjection>>
{
    public Task<IEnumerable<IProjection>> Handle(SearchProductsListItemQuery query, CancellationToken cancellationToken)
        => gateway.SearchAsync<IProjection>(query.Fragment, query.Paging, cancellationToken);
}