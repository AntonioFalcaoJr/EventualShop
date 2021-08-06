using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections.Pagination;
using Application.EventSourcing.Customers.Projections;
using Application.UseCases.Customers.Queries.GetCustomerDetails;

namespace Infrastructure.EventSourcing.Customers.Projections
{
    public class CustomerProjectionsService : ICustomerProjectionsService
    {
        private readonly ICustomerProjectionsRepository _repository;

        public CustomerProjectionsService(ICustomerProjectionsRepository repository)
        {
            _repository = repository;
        }

        public Task<IPagedResult<CustomerDetailsProjection>> GetCustomersDetailsWithPaginationAsync(IPaging paging, Expression<Func<CustomerDetailsProjection, bool>> predicate, CancellationToken cancellationToken)
            => _repository.GetAllAsync(paging, predicate, cancellationToken);

        public Task ProjectNewCustomerDetailsAsync(CustomerDetailsProjection customerDetails, CancellationToken cancellationToken)
            => _repository.SaveAsync(customerDetails, cancellationToken);

        public Task ProjectCustomerListAsync(CustomerDetailsProjection customerDetails, CancellationToken cancellationToken)
            => throw new System.NotImplementedException();

        public Task<CustomerDetailsProjection> GetCustomerDetailsAsync(Guid customerId, CancellationToken cancellationToken)
            => _repository.GetAsync<CustomerDetailsProjection, Guid>(customerId, cancellationToken);
    }
}