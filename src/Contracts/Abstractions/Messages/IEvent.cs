using System.Text.Json.Serialization;
using MassTransit;
using Account = Contracts.Services.Account;
using Identity = Contracts.Services.Identity;
using Catalog = Contracts.Services.Catalog;
using Communication = Contracts.Services.Communication;
using Order = Contracts.Services.Order;
using Payment = Contracts.Services.Payment;
using ShoppingCart = Contracts.Services.ShoppingCart;
using Warehouse = Contracts.Services.Warehouse;

namespace Contracts.Abstractions.Messages;

[ExcludeFromTopology]
[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization)]
[JsonDerivedType(typeof(Account.DomainEvent.AccountDeleted), nameof(Account.DomainEvent.AccountDeleted))]
[JsonDerivedType(typeof(Account.DomainEvent.BillingAddressDeleted), nameof(Account.DomainEvent.BillingAddressDeleted))]
[JsonDerivedType(typeof(Account.DomainEvent.ShippingAddressDeleted), nameof(Account.DomainEvent.ShippingAddressDeleted))]
[JsonDerivedType(typeof(Account.DomainEvent.AccountDeactivated), nameof(Account.DomainEvent.AccountDeactivated))]
[JsonDerivedType(typeof(Account.DomainEvent.AccountCreated), nameof(Account.DomainEvent.AccountCreated))]
[JsonDerivedType(typeof(Account.DomainEvent.AccountActivated), nameof(Account.DomainEvent.AccountActivated))]
[JsonDerivedType(typeof(Account.DomainEvent.BillingAddressAdded), nameof(Account.DomainEvent.BillingAddressAdded))]
[JsonDerivedType(typeof(Account.DomainEvent.BillingAddressRestored), nameof(Account.DomainEvent.BillingAddressRestored))]
[JsonDerivedType(typeof(Account.DomainEvent.ShippingAddressAdded), nameof(Account.DomainEvent.ShippingAddressAdded))]
[JsonDerivedType(typeof(Account.DomainEvent.ShippingAddressRestored), nameof(Account.DomainEvent.ShippingAddressRestored))]
[JsonDerivedType(typeof(Account.DomainEvent.BillingAddressPreferred), nameof(Account.DomainEvent.BillingAddressPreferred))]
[JsonDerivedType(typeof(Account.DomainEvent.ShippingAddressPreferred), nameof(Account.DomainEvent.ShippingAddressPreferred))]
[JsonDerivedType(typeof(Catalog.DomainEvent.CatalogCreated), nameof(Catalog.DomainEvent.CatalogCreated))]
[JsonDerivedType(typeof(Catalog.DomainEvent.CatalogDeleted), nameof(Catalog.DomainEvent.CatalogDeleted))]
[JsonDerivedType(typeof(Catalog.DomainEvent.CatalogActivated), nameof(Catalog.DomainEvent.CatalogActivated))]
[JsonDerivedType(typeof(Catalog.DomainEvent.CatalogDeactivated), nameof(Catalog.DomainEvent.CatalogDeactivated))]
[JsonDerivedType(typeof(Catalog.DomainEvent.CatalogTitleChanged), nameof(Catalog.DomainEvent.CatalogTitleChanged))]
[JsonDerivedType(typeof(Catalog.DomainEvent.CatalogDescriptionChanged), nameof(Catalog.DomainEvent.CatalogDescriptionChanged))]
[JsonDerivedType(typeof(Catalog.DomainEvent.CatalogItemAdded), nameof(Catalog.DomainEvent.CatalogItemAdded))]
[JsonDerivedType(typeof(Catalog.DomainEvent.CatalogItemRemoved), nameof(Catalog.DomainEvent.CatalogItemRemoved))]
[JsonDerivedType(typeof(Catalog.DomainEvent.CatalogItemIncreased), nameof(Catalog.DomainEvent.CatalogItemIncreased))]
[JsonDerivedType(typeof(Communication.DomainEvent.EmailConfirmationRequested), nameof(Communication.DomainEvent.EmailConfirmationRequested))]
[JsonDerivedType(typeof(Identity.DelayedEvent.EmailConfirmationExpired), nameof(Identity.DelayedEvent.EmailConfirmationExpired))]
[JsonDerivedType(typeof(Identity.DomainEvent.UserDeleted), nameof(Identity.DomainEvent.UserDeleted))]
[JsonDerivedType(typeof(Identity.DomainEvent.UserRegistered), nameof(Identity.DomainEvent.UserRegistered))]
[JsonDerivedType(typeof(Identity.DomainEvent.EmailChanged), nameof(Identity.DomainEvent.EmailChanged))]
[JsonDerivedType(typeof(Identity.DomainEvent.PasswordChanged), nameof(Identity.DomainEvent.PasswordChanged))]
[JsonDerivedType(typeof(Identity.DomainEvent.EmailConfirmed), nameof(Identity.DomainEvent.EmailConfirmed))]
[JsonDerivedType(typeof(Identity.DomainEvent.EmailExpired), nameof(Identity.DomainEvent.EmailExpired))]
[JsonDerivedType(typeof(Identity.DomainEvent.PrimaryEmailDefined), nameof(Identity.DomainEvent.PrimaryEmailDefined))]
[JsonDerivedType(typeof(Order.DomainEvent.OrderPlaced), nameof(Order.DomainEvent.OrderPlaced))]
[JsonDerivedType(typeof(Order.DomainEvent.OrderConfirmed), nameof(Order.DomainEvent.OrderConfirmed))]
[JsonDerivedType(typeof(Payment.DomainEvent.PaymentRequested), nameof(Payment.DomainEvent.PaymentRequested))]
[JsonDerivedType(typeof(Payment.DomainEvent.PaymentCanceled), nameof(Payment.DomainEvent.PaymentCanceled))]
[JsonDerivedType(typeof(Payment.DomainEvent.PaymentCompleted), nameof(Payment.DomainEvent.PaymentCompleted))]
[JsonDerivedType(typeof(Payment.DomainEvent.PaymentNotCompleted), nameof(Payment.DomainEvent.PaymentNotCompleted))]
[JsonDerivedType(typeof(Payment.DomainEvent.PaymentMethodAuthorized), nameof(Payment.DomainEvent.PaymentMethodAuthorized))]
[JsonDerivedType(typeof(Payment.DomainEvent.PaymentMethodDenied), nameof(Payment.DomainEvent.PaymentMethodDenied))]
[JsonDerivedType(typeof(Payment.DomainEvent.PaymentMethodRefunded), nameof(Payment.DomainEvent.PaymentMethodRefunded))]
[JsonDerivedType(typeof(Payment.DomainEvent.PaymentMethodRefundDenied), nameof(Payment.DomainEvent.PaymentMethodRefundDenied))]
[JsonDerivedType(typeof(Payment.DomainEvent.PaymentMethodCancellationDenied), nameof(Payment.DomainEvent.PaymentMethodCancellationDenied))]
[JsonDerivedType(typeof(Payment.DomainEvent.PaymentMethodCanceled), nameof(Payment.DomainEvent.PaymentMethodCanceled))]
[JsonDerivedType(typeof(ShoppingCart.DomainEvent.CartCreated), nameof(ShoppingCart.DomainEvent.CartCreated))]
[JsonDerivedType(typeof(ShoppingCart.DomainEvent.CartItemAdded), nameof(ShoppingCart.DomainEvent.CartItemAdded))]
[JsonDerivedType(typeof(ShoppingCart.DomainEvent.CartItemIncreased), nameof(ShoppingCart.DomainEvent.CartItemIncreased))]
[JsonDerivedType(typeof(ShoppingCart.DomainEvent.CartItemDecreased), nameof(ShoppingCart.DomainEvent.CartItemDecreased))]
[JsonDerivedType(typeof(ShoppingCart.DomainEvent.CartItemRemoved), nameof(ShoppingCart.DomainEvent.CartItemRemoved))]
[JsonDerivedType(typeof(ShoppingCart.DomainEvent.CartCheckedOut), nameof(ShoppingCart.DomainEvent.CartCheckedOut))]
[JsonDerivedType(typeof(ShoppingCart.DomainEvent.ShippingAddressAdded), nameof(ShoppingCart.DomainEvent.ShippingAddressAdded))]
[JsonDerivedType(typeof(ShoppingCart.DomainEvent.BillingAddressAdded), nameof(ShoppingCart.DomainEvent.BillingAddressAdded))]
[JsonDerivedType(typeof(ShoppingCart.DomainEvent.PaymentMethodAdded), nameof(ShoppingCart.DomainEvent.PaymentMethodAdded))]
[JsonDerivedType(typeof(ShoppingCart.DomainEvent.CartDiscarded), nameof(ShoppingCart.DomainEvent.CartDiscarded))]
[JsonDerivedType(typeof(ShoppingCart.IntegrationEvent.ProjectionRebuilt), nameof(ShoppingCart.IntegrationEvent.ProjectionRebuilt))]
[JsonDerivedType(typeof(ShoppingCart.IntegrationEvent.CartSubmitted), nameof(ShoppingCart.IntegrationEvent.CartSubmitted))]
[JsonDerivedType(typeof(Warehouse.DomainEvent.InventoryCreated), nameof(Warehouse.DomainEvent.InventoryCreated))]
[JsonDerivedType(typeof(Warehouse.DomainEvent.InventoryItemReceived), nameof(Warehouse.DomainEvent.InventoryItemReceived))]
[JsonDerivedType(typeof(Warehouse.DomainEvent.InventoryAdjustmentIncreased), nameof(Warehouse.DomainEvent.InventoryAdjustmentIncreased))]
[JsonDerivedType(typeof(Warehouse.DomainEvent.InventoryAdjustmentDecreased), nameof(Warehouse.DomainEvent.InventoryAdjustmentDecreased))]
[JsonDerivedType(typeof(Warehouse.DomainEvent.InventoryAdjustmentNotDecreased), nameof(Warehouse.DomainEvent.InventoryAdjustmentNotDecreased))]
[JsonDerivedType(typeof(Warehouse.DomainEvent.InventoryReserved), nameof(Warehouse.DomainEvent.InventoryReserved))]
[JsonDerivedType(typeof(Warehouse.DomainEvent.StockDepleted), nameof(Warehouse.DomainEvent.StockDepleted))]
[JsonDerivedType(typeof(Warehouse.DomainEvent.InventoryNotReserved), nameof(Warehouse.DomainEvent.InventoryNotReserved))]
[JsonDerivedType(typeof(Warehouse.DomainEvent.InventoryItemIncreased), nameof(Warehouse.DomainEvent.InventoryItemIncreased))]
[JsonDerivedType(typeof(Warehouse.DomainEvent.InventoryItemDecreased), nameof(Warehouse.DomainEvent.InventoryItemDecreased))]
public interface IEvent : IMessage
{
}