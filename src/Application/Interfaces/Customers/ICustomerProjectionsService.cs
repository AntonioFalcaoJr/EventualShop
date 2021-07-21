using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.UseCases.Customers.Queries.GetCustomers;

namespace Application.Interfaces.Customers
{
    public interface ICustomerProjectionsService
    {
        Task<IEnumerable<Models.CustomerModel>> GetCustomersAsync();
        Task ProjectNewCustomerAsync(Models.CustomerModel customerModel, CancellationToken contextCancellationToken);
        Task ProjectCustomerListAsync(Models.CustomerModel customerModel, CancellationToken contextCancellationToken);
    }
}