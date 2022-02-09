using System;
using System.Collections.Generic;
using ECommerce.Abstractions.Messages.Queries.Responses;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.ShoppingCart;

public static class Responses
{
    public record NotFound(string Message = "Not found.") : Response;

    public record CartDetails : Response
    {
        public IEnumerable<Models.Item> CartItems { get; init; }
        public IEnumerable<Models.IPaymentMethod> PaymentMethods { get; init; }
        public Guid CustomerId { get; init; }
        public decimal Total { get; init; }
        public Guid Id { get; init; }
        public bool IsDeleted { get; init; }
    }
}