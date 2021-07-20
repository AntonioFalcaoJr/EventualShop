using System.Collections.Generic;

namespace Application.UseCases.Customers.Queries.GetCustomers
{
    public static class Models
    {
        public record CustomersModel;

        public record GetCustomersModel(IEnumerable<CustomersModel> Customers);
    }
}