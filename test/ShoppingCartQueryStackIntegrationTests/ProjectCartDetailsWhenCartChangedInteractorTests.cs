using System.Globalization;
using Contracts.Abstractions.Protobuf;
using Contracts.Services.ShoppingCart;
using Contracts.Services.ShoppingCart.Protobuf;
using FluentAssertions;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Xunit;
using static Contracts.Services.ShoppingCart.Protobuf.ShoppingCartService;

namespace ShoppingCartQueryStackIntegrationTests;

public class ProjectCartDetailsWhenCartChangedInteractorTests : IClassFixture<ShoppingCartServiceClientFixture>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ShoppingCartServiceClient _client;

    public ProjectCartDetailsWhenCartChangedInteractorTests(ShoppingCartServiceClientFixture factory)
    {
        _client = factory.Services.GetRequiredService<ShoppingCartServiceClient>();
        _publishEndpoint = factory.Services.GetRequiredService<IPublishEndpoint>();
    }

    [Fact]
    public async Task CartDetailsShouldBeProjectedWhenCartCreated()
    {
        // Given
        DomainEvent.CartCreated @event = new(
            CartId: Guid.NewGuid(),
            CustomerId: Guid.NewGuid(),
            Total: new(decimal.Zero.ToString(CultureInfo.InvariantCulture), "USD"),
            Status: "Empty",
            Version: 1);

        // When
        await _publishEndpoint.Publish(@event, CancellationToken.None);

        // Then
        var response = await Policy
            .HandleResult<GetResponse>(response => response.OneOfCase is GetResponse.OneOfOneofCase.NotFound)
            .RetryAsync()
            .ExecuteAsync(async () => await _client.GetShoppingCartDetailsAsync(new() { CartId = @event.CartId.ToString() }));

        response.NotFound.Should().BeNull();
        response.Projection.TryUnpack<ShoppingCartDetails>(out var projection).Should().BeTrue();

        projection.CartId.Should().Be(@event.CartId.ToString());
        projection.CustomerId.Should().Be(@event.CustomerId.ToString());
        projection.Total.Should().BeEquivalentTo(@event.Total);
        projection.Status.Should().Be(@event.Status);
    }
}