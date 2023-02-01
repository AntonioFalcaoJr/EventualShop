using Application.Abstractions;
using Contracts.Services.ShoppingCart;
using FluentAssertions;
using Grpc.Net.Client;
using GrpcService;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ShoppingCartQueryStackIntegrationTests;

public class ProjectCartDetailsWhenCartChangedInteractorTests : IClassFixture<TestingWebApplicationFactory<Program>>
{
    private static readonly TimeSpan Delay = TimeSpan.FromSeconds(1);
    private readonly IPublishEndpoint _bus;

    public ProjectCartDetailsWhenCartChangedInteractorTests()
    {
        var factory = new TestingWebApplicationFactory<Program>();
        _bus = factory.Services.GetRequiredService<IPublishEndpoint>();
        var client = factory.CreateDefaultClient();
        var grpcChannel = GrpcChannel.ForAddress(client.BaseAddress, new() { HttpClient = client });
        var service = grpcChannel.
    }

    [Fact]
    public async Task CartDetailsShouldBeProjectedWhenCartCreated()
    {
        DomainEvent.CartCreated @event = new(Guid.NewGuid(), Guid.NewGuid(), new("0.00", "USD"), "Empty", 1);

        // Act
        await _bus.Publish(@event, CancellationToken.None);
        await Task.Delay(Delay);

        // Assert
        var projection = await _projectionGateway.GetAsync(@event.CartId, CancellationToken.None);

        projection.Should().NotBeNull();
        projection!.Id.Should().Be(@event.CartId);
        projection.CustomerId.Should().Be(@event.CustomerId);
        projection.Total.Should().Be(@event.Total);
        projection.Status.Should().Be(@event.Status);
        projection.IsDeleted.Should().BeFalse();
        projection.Version.Should().Be(@event.Version);
    }
}