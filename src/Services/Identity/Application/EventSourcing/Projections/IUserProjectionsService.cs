using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections;

namespace Application.EventSourcing.Projections;

public interface IUserProjectionsService : IProjectionsService
{
    Task<UserAuthenticationDetailsProjection> GetUserAuthenticationDetailsAsync(Guid userId, CancellationToken cancellationToken);
}