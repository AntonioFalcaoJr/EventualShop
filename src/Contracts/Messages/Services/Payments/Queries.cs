using System;
using Messages.Abstractions.Queries;

namespace Messages.Services.Payments;

public static class Queries
{
    public record GetPaymentDetails(Guid PaymentId) : Query;
}