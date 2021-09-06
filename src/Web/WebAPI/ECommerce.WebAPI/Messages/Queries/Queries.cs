using System;
using Messages.Customers.Queries;

namespace ECommerce.WebAPI.Messages.Queries
{
    public static class Queries
    {
        public record GetCustomerDetailsQuery(Guid Id) : GetCustomerDetails;
        public record GetCustomersDetailsWithPaginationQuery(int Limit, int Offset) : GetCustomersDetailsWithPagination;
    }
}