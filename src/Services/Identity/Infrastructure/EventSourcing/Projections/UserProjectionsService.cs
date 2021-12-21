using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections;
using Application.EventSourcing.Projections;

namespace Infrastructure.EventSourcing.Projections;

public class UserProjectionsService : IUserProjectionsService
{
    private readonly IUserProjectionsRepository _repository;

    public UserProjectionsService(IUserProjectionsRepository repository)
    {
        _repository = repository;
    }

    public Task<UserAuthenticationDetailsProjection> GetUserAuthenticationDetailsAsync(Guid userId, CancellationToken cancellationToken)
        => _repository.FindAsync<UserAuthenticationDetailsProjection>(
            predicate: projection => projection.Id.Equals(userId) &&
                                     projection.IsDeleted == false,
            cancellationToken: cancellationToken);
    
    public Task ProjectAsync<TProjection>(TProjection projection, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _repository.UpsertAsync(projection, cancellationToken);

    public Task RemoveAsync<TProjection>(Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _repository.DeleteAsync(filter, cancellationToken);
}