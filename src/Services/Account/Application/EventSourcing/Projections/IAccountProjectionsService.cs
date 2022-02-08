using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections;
using ECommerce.Abstractions.Messages.Queries.Paging;

namespace Application.EventSourcing.Projections;

public interface IAccountProjectionsService : IProjectionsService
{
    Task<IPagedResult<AccountDetailsProjection>> GetAccountsAsync(IPaging paging, CancellationToken cancellationToken);
    Task<AccountDetailsProjection> GetAccountDetailsAsync(Guid accountId, CancellationToken cancellationToken);
}