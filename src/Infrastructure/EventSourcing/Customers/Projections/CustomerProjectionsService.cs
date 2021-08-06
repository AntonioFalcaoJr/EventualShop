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

        public Task<IPagedResult<CustomerDetailsModel>> GetCustomersWithPaginationAsync(IPaging paging, Expression<Func<CustomerDetailsModel, bool>> predicate, CancellationToken cancellationToken)
            => _repository.GetAllAsync(paging, predicate, cancellationToken);

        public Task ProjectNewCustomerDetailsAsync(CustomerDetailsModel customerDetails, CancellationToken cancellationToken)
            => _repository.SaveAsync(customerDetails, cancellationToken);

        public Task ProjectCustomerListAsync(CustomerDetailsModel customerDetails, CancellationToken cancellationToken)
            => throw new System.NotImplementedException();

        public Task<CustomerDetailsModel> GetCustomerDetailsAsync(Guid customerId, CancellationToken cancellationToken)
            => _repository.GetAsync<CustomerDetailsModel, Guid>(customerId, cancellationToken);
    }
}