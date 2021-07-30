using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.UseCases.Customers.Queries.GetCustomers;

namespace Application.EventSourcing.Customers.Projections
{
    public interface ICustomerProjectionsService
    {
        Task<List<Models.CustomerDetail>> GetCustomersAsync(Expression<Func<Models.CustomerDetail, bool>> predicate, CancellationToken cancellationToken);
        Task ProjectNewCustomerAsync(Models.CustomerDetail customerDetail, CancellationToken cancellationToken);
        Task ProjectCustomerListAsync(Models.CustomerDetail customerDetail, CancellationToken cancellationToken);
        Task<Models.CustomerDetail> GetCustomerDetailAsync(Guid customerId, CancellationToken cancellationToken);
    }
}