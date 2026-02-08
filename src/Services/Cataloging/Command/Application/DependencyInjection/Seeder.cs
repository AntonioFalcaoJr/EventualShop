using Application.UseCases.CatalogItems.Commands;
using Application.UseCases.Catalogs.Commands;
using Contracts.Boundaries.Warehouse.Inventory;
using Domain.Aggregates;
using Domain.Aggregates.Products;
using Domain.ValueObjects;
using MediatR;
using Bogus;
using Version = Domain.ValueObjects.Version;

namespace Application.DependencyInjection;

public interface ISeeder
{
    ValueTask SeedDataAsync(CancellationToken token);
}

public class Seeder(ISender sender) : ISeeder
{
    public async ValueTask SeedDataAsync(CancellationToken token)
    {
        var appId = AppId.New;
        var faker = new Faker();

        var events = new Faker<DomainEvent.InventoryItemReceived>()
            .CustomInstantiator(fake => new(
                InventoryItemId.New,
                InventoryItemId.New,
                ProductId.New,
                ProductName.Undefined,
                Brand.Undefined,
                fake.Commerce.Categories(1)[0],
                fake.Commerce.ProductMaterial(),
                Currency.USD,
                fake.Finance.Amount(1, 100).ToString("F2"),
                fake.Random.Int(1, 500),
                fake.Commerce.Ean13(),
                Version.Initial))
            .Generate(10);

        foreach (var @event in events) await sender.Send(@event, token);

        // Register a catalog
        RegisterCatalog registerCatalog = new(appId, (Title)faker.Commerce.Department(), (Description)faker.Lorem.Sentence());
        var catalogId = await sender.Send(registerCatalog, token);

        // Associate catalog items
        for (var i = 0; i < 10; i++)
        {
            var associateCatalogItem = new AssociateCatalogItem(
                appId,
                catalogId,
                (InventoryItemId)events[i].InventoryItemId,
                Quantity.Number(faker.Random.Int(1, 10))
            );

            await sender.Send(associateCatalogItem, token);
        }
    }
}