using Contracts.Abstractions.Messages;

namespace Contracts.Boundaries.Shopping.Product;

public static class DomainEvent
{
    public record ProductReleased(string ProductId, string CatalogItemId, string Name, IDictionary<string, string> Prices, string Availability, ulong Version) 
        : Message, IDomainEvent;

    public record ProductTaken(string ProductId, string Quantity, string NewInventory, ulong Version) 
        : Message, IDomainEvent;

    public record ProductRestocked(string ProductId, string Quantity, string NewAvailability, ulong Version) 
        : Message, IDomainEvent;

    public record ProductReserved(string ProductId, string CartId, string Quantity, string NewReservation, string NewTotalReserved, ulong Version)
        : Message, IDomainEvent;
    
    public record ReservationReleased(string ProductId, string CartId, string NewReservation, string NewTotalReserved, ulong Version)
        : Message, IDomainEvent;
    
    public record ReservationDecreased(string ProductId, string CartId, string NewReservation, string NewTotalReserved, ulong Version)
        : Message, IDomainEvent;
    
    public record ReservationIncreased(string ProductId, string CartId, string NewReservation, string NewTotalReserved, ulong Version)
        : Message, IDomainEvent;
}