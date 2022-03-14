using System;
using System.Net.Http;
using System.Threading.Tasks;
using ECommerce.WebAPI.IntegrationTests.Factories;
using WebAPI;
using Xunit;

namespace ECommerce.WebAPI.IntegrationTests;

public class ShoppingCartIntegrationTests : IClassFixture<TestingWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ShoppingCartIntegrationTests(TestingWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Test1()
    {
        var httpResponseMessage = await _client.PutAsync($"api/v1/shopping-carts/{Guid.NewGuid()}/check-out", default);
        httpResponseMessage.EnsureSuccessStatusCode();
    }
}