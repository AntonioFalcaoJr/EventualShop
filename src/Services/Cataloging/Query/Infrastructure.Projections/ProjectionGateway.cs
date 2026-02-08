using System.Linq.Expressions;
using Application.Abstractions;
using Contracts.Abstractions;
using Contracts.Abstractions.Paging;
using Elastic.Clients.Elasticsearch;

namespace Infrastructure.Projections;

public class ProjectionGateway<TProjection>(ElasticsearchClient client) : IProjectionGateway<TProjection>
    where TProjection : IProjection
{
    private static string IndexName => typeof(TProjection).Name.ToLower();
    
    public Task IndexAsync(TProjection projection, CancellationToken token) 
        => client.IndexAsync(projection, index => index.Index(IndexName), token);

    public Task<TProjection?> GetAsync<TId>(TId id, CancellationToken cancellationToken) 
        => FindAsync(projection => projection.Id.Equals(id), cancellationToken);

    public ValueTask<IPagedResult<TProjection>> ListAsync(Paging paging, Expression<Func<TProjection, bool>> predicate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public ValueTask<IPagedResult<TProjection>> ListAsync(Paging paging, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public ValueTask ReplaceInsertAsync(TProjection replacement, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public ValueTask RebuildInsertAsync(TProjection replacement, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync<TId>(TId id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateFieldAsync<TField, TId>(TId id, ulong version, Expression<Func<TProjection, TField>> field, TField value,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TProjection>> SearchAsync(string fragment, Paging paging, CancellationToken cancellationToken)
    {
        var response = await client.SearchAsync<TProjection>(descriptor
                => descriptor.Query(query
                        => query.Match(match
                            => match.Query(fragment)))
                    .From(paging.Number)
                    .Size(paging.Size),
            cancellationToken).WaitAsync(cancellationToken);

        return response.Documents;
    }

    // public async ValueTask<IPagedResult<IProjection<THit>>> SearchAsync<THit>(Paging paging, CancellationToken token)
    //     where THit : class
    // {
    //     var from = (paging.Number - 1) * paging.Size;
    //     var size = paging.Size + 1;
    //
    //     var response = await client.SearchAsync<THit>(search =>
    //     {
    //         search
    //             .Index(IndexName)
    //             .Query(query => query
    //                 .QueryString(queryString => queryString
    //                     .Fields(request.Fields)
    //                     .Query(request.Query)));
    //
    //         search
    //             .Highlight(highlight => highlight
    //                 .Fields(fields =>
    //                 {
    //                     foreach (var field in request.Fields)
    //                         fields.Add(field, new HighlightFieldDescriptor<THit>());
    //
    //                     return fields;
    //                 }));
    //
    //         search.From(from).Size(size);
    //         
    //     }, token);
    //
    //     if (response.IsValidResponse is false)
    //         throw new InvalidOperationException(response.ElasticsearchServerError?.Error.Reason);
    //
    //     var projections = response.Hits.Select(hit => new Projection<THit>(hit.Source!, hit.Highlight!));
    //
    //     return PagedResult<IProjection<THit>>.Create(projections, request.Paging);
    // }
    
    public Task<TProjection?> FindAsync(Expression<Func<TProjection, bool>> predicate, CancellationToken cancellationToken)
    {
        // var response = await client.SearchAsync<TProjection>(descriptor
        //         => descriptor.Query(query
        //             => query.MatchAll()).Size(1),
        //     cancellationToken);
        //
        // return response.Documents.FirstOrDefault();
        
        throw new NotImplementedException();
    }
}