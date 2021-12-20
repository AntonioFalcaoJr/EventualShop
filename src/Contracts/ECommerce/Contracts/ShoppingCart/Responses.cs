using ECommerce.Abstractions.Queries.Responses;
using ECommerce.Contracts.Common;
using Newtonsoft.Json;

namespace ECommerce.Contracts.ShoppingCart;

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