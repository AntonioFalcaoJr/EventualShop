using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.EventSourcing.Customers.Projections;
using Application.UseCases.Customers.Queries.CustomerDetails;

namespace Infrastructure.EventSourcing.Customers.Projections
{
    public class CustomerProjectionsService : ICustomerProjectionsService
    {
        private readonly ICustomerProjectionsRepository _repository;

        public CustomerProjectionsService(ICustomerProjectionsRepository repository)
        {
            _repository = repository;
        }

        public Task<List<CustomerDetailsModel>> GetCustomersAsync(Expression<Func<CustomerDetailsModel, bool>> predicate, CancellationToken cancellationToken)
            => _repository.GetAllAsync(predicate, cancellationToken);

        public Task ProjectNewCustomerDetailsAsync(CustomerDetailsModel customerDetails, CancellationToken cancellationToken)
            => _repository.SaveAsync(customerDetails, cancellationToken);

        public Task ProjectCustomerListAsync(CustomerDetailsModel customerDetails, CancellationToken cancellationToken)
            => throw new System.NotImplementedException();

        public Task<CustomerDetailsModel> GetCustomerDetailsAsync(Guid customerId, CancellationToken cancellationToken)
            => _repository.GetAsync<CustomerDetailsModel, Guid>(customerId, cancellationToken);
    }
}