using System;
using System.Collections.Generic;
using Messages.Abstractions.Queries.Responses;
using Newtonsoft.Json;

namespace Messages.Services.ShoppingCarts;

public static class Responses
{
    public record CartDetails : Response
    {
        public IEnumerable<Models.Item> CartItems { get; init; }

        [JsonProperty(TypeNameHandling = TypeNameHandling.Objects)]
        public IEnumerable<Models.IPaymentMethod> PaymentMethods { get; init; }

        public Guid UserId { get; init; }
        public decimal Total { get; init; }
        public Guid Id { get; init; }
        public bool IsDeleted { get; init; }
    }
}