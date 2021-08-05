using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.UseCases.Customers.Queries.CustomerDetails;

namespace Application.EventSourcing.Customers.Projections
{
    public interface ICustomerProjectionsService 
    {
        Task<List<CustomerDetailsModel>> GetCustomersAsync(Expression<Func<CustomerDetailsModel, bool>> predicate, CancellationToken cancellationToken);
        Task ProjectNewCustomerDetailsAsync(CustomerDetailsModel customerDetails, CancellationToken cancellationToken);
        Task ProjectCustomerListAsync(CustomerDetailsModel customerDetails, CancellationToken cancellationToken);
        Task<CustomerDetailsModel> GetCustomerDetailsAsync(Guid customerId, CancellationToken cancellationToken);
    }
}