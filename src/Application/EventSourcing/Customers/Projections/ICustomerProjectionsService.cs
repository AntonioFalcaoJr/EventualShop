using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections.Pagination;
using Application.UseCases.Customers.Queries.GetCustomerDetails;

namespace Application.EventSourcing.Customers.Projections
{
    public interface ICustomerProjectionsService 
    {
        Task<IPagedResult<CustomerDetailsProjection>> GetCustomersDetailsWithPaginationAsync(IPaging paging, Expression<Func<CustomerDetailsProjection, bool>> predicate, CancellationToken cancellationToken);
        Task ProjectNewCustomerDetailsAsync(CustomerDetailsProjection customerDetails, CancellationToken cancellationToken);
        Task ProjectCustomerListAsync(CustomerDetailsProjection customerDetails, CancellationToken cancellationToken);
        Task<CustomerDetailsProjection> GetCustomerDetailsAsync(Guid customerId, CancellationToken cancellationToken);
    }
}