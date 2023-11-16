using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Boundaries.Shopping.ShoppingCart;

public static class SummaryEvent
{
    public record CartProjectionRebuilt(Dto.ShoppingCart Cart, string Version) : Message, ISummaryEvent;

    public record CartSubmitted(Dto.ShoppingCart Cart, string Version) : Message, ISummaryEvent;
}