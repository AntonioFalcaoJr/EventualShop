using System;
using Domain.Abstractions.Aggregates;
using Domain.Enumerations;
using Domain.ValueObjects.Addresses;
using Domain.ValueObjects.CreditCards;
using Messages.Abstractions.Events;
using Messages.Payments;

namespace Domain.Aggregates
{
    public class Payment : AggregateRoot<Guid>
    {
        public Guid OrderId { get; private set; }
        public decimal Amount { get; private set; }
        public PaymentStatus Status { get; private set; }
        public Address BillingAddress { get; private set; }
        public CreditCard CreditCard { get; private set; }

        public void Handle(Commands.RequestPayment cmd)
            => RaiseEvent(new Events.PaymentRequested(Id, cmd.OrderId, cmd.Amount, cmd.BillingAddress, cmd.CreditCard));
        
        public void Handle(Commands.CancelPayment cmd)
            => RaiseEvent(new Events.PaymentCanceled(Id, cmd.OrderId));

        protected override void ApplyEvent(IEvent @event)
            => When(@event as dynamic);

        private void When(Events.PaymentRequested @event)
        {
            Id = @event.PaymentId;
            OrderId = @event.OrderId;
            Amount = @event.Amount;

            BillingAddress = new()
            {
                City = @event.BillingAddress.City,
                Country = @event.BillingAddress.Country,
                Number = @event.BillingAddress.Number,
                State = @event.BillingAddress.State,
                Street = @event.BillingAddress.Street,
                ZipCode = @event.BillingAddress.ZipCode
            };

            CreditCard = new()
            {
                Expiration = @event.CreditCard.Expiration,
                Number = @event.CreditCard.Number,
                HolderName = @event.CreditCard.HolderName,
                SecurityNumber = @event.CreditCard.SecurityNumber
            };

            Status = PaymentStatus.Pending;
        }

        protected override bool Validate()
            => true;
    }
}