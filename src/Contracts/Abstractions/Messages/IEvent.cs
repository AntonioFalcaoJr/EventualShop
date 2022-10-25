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
[JsonDerivedType(typeof(Account.DomainEvent.AccountDeleted), $"Account.{nameof(Account.DomainEvent.AccountDeleted)}")]
[JsonDerivedType(typeof(Account.DomainEvent.BillingAddressDeleted), $"Account.{nameof(Account.DomainEvent.BillingAddressDeleted)}")]
[JsonDerivedType(typeof(Account.DomainEvent.ShippingAddressDeleted), $"Account.{nameof(Account.DomainEvent.ShippingAddressDeleted)}")]
[JsonDerivedType(typeof(Account.DomainEvent.AccountDeactivated), $"Account.{nameof(Account.DomainEvent.AccountDeactivated)}")]
[JsonDerivedType(typeof(Account.DomainEvent.AccountCreated), $"Account.{nameof(Account.DomainEvent.AccountCreated)}")]
[JsonDerivedType(typeof(Account.DomainEvent.AccountActivated), $"Account.{nameof(Account.DomainEvent.AccountActivated)}")]
[JsonDerivedType(typeof(Account.DomainEvent.BillingAddressAdded), $"Account.{nameof(Account.DomainEvent.BillingAddressAdded)}")]
[JsonDerivedType(typeof(Account.DomainEvent.BillingAddressRestored), $"Account.{nameof(Account.DomainEvent.BillingAddressRestored)}")]
[JsonDerivedType(typeof(Account.DomainEvent.ShippingAddressAdded), $"Account.{nameof(Account.DomainEvent.ShippingAddressAdded)}")]
[JsonDerivedType(typeof(Account.DomainEvent.ShippingAddressRestored), $"Account.{nameof(Account.DomainEvent.ShippingAddressRestored)}")]
[JsonDerivedType(typeof(Account.DomainEvent.BillingAddressPreferred), $"Account.{nameof(Account.DomainEvent.BillingAddressPreferred)}")]
[JsonDerivedType(typeof(Account.DomainEvent.ShippingAddressPreferred), $"Account.{nameof(Account.DomainEvent.ShippingAddressPreferred)}")]
[JsonDerivedType(typeof(Catalog.DomainEvent.CatalogCreated), $"Catalog.{nameof(Catalog.DomainEvent.CatalogCreated)}")]
[JsonDerivedType(typeof(Catalog.DomainEvent.CatalogDeleted), $"Catalog.{nameof(Catalog.DomainEvent.CatalogDeleted)}")]
[JsonDerivedType(typeof(Catalog.DomainEvent.CatalogActivated), $"Catalog.{nameof(Catalog.DomainEvent.CatalogActivated)}")]
[JsonDerivedType(typeof(Catalog.DomainEvent.CatalogDeactivated), $"Catalog.{nameof(Catalog.DomainEvent.CatalogDeactivated)}")]
[JsonDerivedType(typeof(Catalog.DomainEvent.CatalogTitleChanged), $"Catalog.{nameof(Catalog.DomainEvent.CatalogTitleChanged)}")]
[JsonDerivedType(typeof(Catalog.DomainEvent.CatalogDescriptionChanged), $"Catalog.{nameof(Catalog.DomainEvent.CatalogDescriptionChanged)}")]
[JsonDerivedType(typeof(Catalog.DomainEvent.CatalogItemAdded), $"Catalog.{nameof(Catalog.DomainEvent.CatalogItemAdded)}")]
[JsonDerivedType(typeof(Catalog.DomainEvent.CatalogItemRemoved), $"Catalog.{nameof(Catalog.DomainEvent.CatalogItemRemoved)}")]
[JsonDerivedType(typeof(Catalog.DomainEvent.CatalogItemIncreased), $"Catalog.{nameof(Catalog.DomainEvent.CatalogItemIncreased)}")]
[JsonDerivedType(typeof(Communication.DomainEvent.EmailConfirmationRequested), $"Communication.{nameof(Communication.DomainEvent.EmailConfirmationRequested)}")]
[JsonDerivedType(typeof(Identity.DelayedEvent.EmailConfirmationExpired), $"Identity.{nameof(Identity.DelayedEvent.EmailConfirmationExpired)}")]
[JsonDerivedType(typeof(Identity.DomainEvent.UserDeleted), $"Identity.{nameof(Identity.DomainEvent.UserDeleted)}")]
[JsonDerivedType(typeof(Identity.DomainEvent.UserRegistered), $"Identity.{nameof(Identity.DomainEvent.UserRegistered)}")]
[JsonDerivedType(typeof(Identity.DomainEvent.EmailChanged), $"Identity.{nameof(Identity.DomainEvent.EmailChanged)}")]
[JsonDerivedType(typeof(Identity.DomainEvent.PasswordChanged), $"Identity.{nameof(Identity.DomainEvent.PasswordChanged)}")]
[JsonDerivedType(typeof(Identity.DomainEvent.EmailConfirmed), $"Identity.{nameof(Identity.DomainEvent.EmailConfirmed)}")]
[JsonDerivedType(typeof(Identity.DomainEvent.EmailExpired), $"Identity.{nameof(Identity.DomainEvent.EmailExpired)}")]
[JsonDerivedType(typeof(Identity.DomainEvent.PrimaryEmailDefined), $"Identity.{nameof(Identity.DomainEvent.PrimaryEmailDefined)}")]
[JsonDerivedType(typeof(Order.DomainEvent.OrderPlaced), $"Order.{nameof(Order.DomainEvent.OrderPlaced)}")]
[JsonDerivedType(typeof(Order.DomainEvent.OrderConfirmed), $"Order.{nameof(Order.DomainEvent.OrderConfirmed)}")]
[JsonDerivedType(typeof(Payment.DomainEvent.PaymentRequested), $"Payment.{nameof(Payment.DomainEvent.PaymentRequested)}")]
[JsonDerivedType(typeof(Payment.DomainEvent.PaymentCanceled), $"Payment.{nameof(Payment.DomainEvent.PaymentCanceled)}")]
[JsonDerivedType(typeof(Payment.DomainEvent.PaymentCompleted), $"Payment.{nameof(Payment.DomainEvent.PaymentCompleted)}")]
[JsonDerivedType(typeof(Payment.DomainEvent.PaymentNotCompleted), $"Payment.{nameof(Payment.DomainEvent.PaymentNotCompleted)}")]
[JsonDerivedType(typeof(Payment.DomainEvent.PaymentMethodAuthorized), $"Payment.{nameof(Payment.DomainEvent.PaymentMethodAuthorized)}")]
[JsonDerivedType(typeof(Payment.DomainEvent.PaymentMethodDenied), $"Payment.{nameof(Payment.DomainEvent.PaymentMethodDenied)}")]
[JsonDerivedType(typeof(Payment.DomainEvent.PaymentMethodRefunded), $"Payment.{nameof(Payment.DomainEvent.PaymentMethodRefunded)}")]
[JsonDerivedType(typeof(Payment.DomainEvent.PaymentMethodRefundDenied), $"Payment.{nameof(Payment.DomainEvent.PaymentMethodRefundDenied)}")]
[JsonDerivedType(typeof(Payment.DomainEvent.PaymentMethodCancellationDenied), $"Payment.{nameof(Payment.DomainEvent.PaymentMethodCancellationDenied)}")]
[JsonDerivedType(typeof(Payment.DomainEvent.PaymentMethodCanceled), $"Payment.{nameof(Payment.DomainEvent.PaymentMethodCanceled)}")]
[JsonDerivedType(typeof(ShoppingCart.DomainEvent.CartCreated), $"ShoppingCart.{nameof(ShoppingCart.DomainEvent.CartCreated)}")]
[JsonDerivedType(typeof(ShoppingCart.DomainEvent.CartItemAdded), $"ShoppingCart.{nameof(ShoppingCart.DomainEvent.CartItemAdded)}")]
[JsonDerivedType(typeof(ShoppingCart.DomainEvent.CartItemIncreased), $"ShoppingCart.{nameof(ShoppingCart.DomainEvent.CartItemIncreased)}")]
[JsonDerivedType(typeof(ShoppingCart.DomainEvent.CartItemDecreased), $"ShoppingCart.{nameof(ShoppingCart.DomainEvent.CartItemDecreased)}")]
[JsonDerivedType(typeof(ShoppingCart.DomainEvent.CartItemRemoved), $"ShoppingCart.{nameof(ShoppingCart.DomainEvent.CartItemRemoved)}")]
[JsonDerivedType(typeof(ShoppingCart.DomainEvent.CartCheckedOut), $"ShoppingCart.{nameof(ShoppingCart.DomainEvent.CartCheckedOut)}")]
[JsonDerivedType(typeof(ShoppingCart.DomainEvent.ShippingAddressAdded), $"ShoppingCart.{nameof(ShoppingCart.DomainEvent.ShippingAddressAdded)}")]
[JsonDerivedType(typeof(ShoppingCart.DomainEvent.BillingAddressAdded), $"ShoppingCart.{nameof(ShoppingCart.DomainEvent.BillingAddressAdded)}")]
[JsonDerivedType(typeof(ShoppingCart.DomainEvent.PaymentMethodAdded), $"ShoppingCart.{nameof(ShoppingCart.DomainEvent.PaymentMethodAdded)}")]
[JsonDerivedType(typeof(ShoppingCart.DomainEvent.CartDiscarded), $"ShoppingCart.{nameof(ShoppingCart.DomainEvent.CartDiscarded)}")]
[JsonDerivedType(typeof(ShoppingCart.IntegrationEvent.ProjectionRebuilt), $"ShoppingCart.{nameof(ShoppingCart.IntegrationEvent.ProjectionRebuilt)}")]
[JsonDerivedType(typeof(ShoppingCart.IntegrationEvent.CartSubmitted), $"ShoppingCart.{nameof(ShoppingCart.IntegrationEvent.CartSubmitted)}")]
[JsonDerivedType(typeof(Warehouse.DomainEvent.InventoryCreated), $"Warehouse.{nameof(Warehouse.DomainEvent.InventoryCreated)}")]
[JsonDerivedType(typeof(Warehouse.DomainEvent.InventoryItemReceived), $"Warehouse.{nameof(Warehouse.DomainEvent.InventoryItemReceived)}")]
[JsonDerivedType(typeof(Warehouse.DomainEvent.InventoryAdjustmentIncreased), $"Warehouse.{nameof(Warehouse.DomainEvent.InventoryAdjustmentIncreased)}")]
[JsonDerivedType(typeof(Warehouse.DomainEvent.InventoryAdjustmentDecreased), $"Warehouse.{nameof(Warehouse.DomainEvent.InventoryAdjustmentDecreased)}")]
[JsonDerivedType(typeof(Warehouse.DomainEvent.InventoryAdjustmentNotDecreased), $"Warehouse.{nameof(Warehouse.DomainEvent.InventoryAdjustmentNotDecreased)}")]
[JsonDerivedType(typeof(Warehouse.DomainEvent.InventoryReserved), $"Warehouse.{nameof(Warehouse.DomainEvent.InventoryReserved)}")]
[JsonDerivedType(typeof(Warehouse.DomainEvent.StockDepleted), $"Warehouse.{nameof(Warehouse.DomainEvent.StockDepleted)}")]
[JsonDerivedType(typeof(Warehouse.DomainEvent.InventoryNotReserved), $"Warehouse.{nameof(Warehouse.DomainEvent.InventoryNotReserved)}")]
[JsonDerivedType(typeof(Warehouse.DomainEvent.InventoryItemIncreased), $"Warehouse.{nameof(Warehouse.DomainEvent.InventoryItemIncreased)}")]
[JsonDerivedType(typeof(Warehouse.DomainEvent.InventoryItemDecreased), $"Warehouse.{nameof(Warehouse.DomainEvent.InventoryItemDecreased)}")]
public interface IEvent : IMessage
{
}