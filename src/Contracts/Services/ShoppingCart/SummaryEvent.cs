using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.ShoppingCart;

public static class SummaryEvent
{
    public record CartProjectionRebuilt(Dto.ShoppingCart Cart, long Version) : Message, ISummaryEvent;

    public record CartSubmitted(Dto.ShoppingCart Cart, long Version) : Message, ISummaryEvent;
}