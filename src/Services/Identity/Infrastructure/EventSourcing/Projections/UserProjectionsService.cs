using System;
using System.Threading;
using System.Threading.Tasks;
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

    public Task ProjectUserAuthenticationDetailsAsync(UserAuthenticationDetailsProjection userAuthenticationDetails, CancellationToken cancellationToken)
        => _repository.UpsertAsync(userAuthenticationDetails, cancellationToken);
}