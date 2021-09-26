using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.EventSourcing.Projections
{
    public interface IUserProjectionsService
    {
        Task<UserAuthenticationDetailsProjection> GetUserAuthenticationDetailsAsync(Guid userId, CancellationToken cancellationToken);
        Task ProjectUserAuthenticationDetailsAsync(UserAuthenticationDetailsProjection userAuthenticationDetails, CancellationToken cancellationToken);
    }
}