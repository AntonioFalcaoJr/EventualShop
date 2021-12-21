using System;
using ECommerce.Abstractions.Queries;

namespace ECommerce.Contracts.Warehouse;

public static class Queries
{
    public record GetProductDetails(Guid ProductId) : Query;
}