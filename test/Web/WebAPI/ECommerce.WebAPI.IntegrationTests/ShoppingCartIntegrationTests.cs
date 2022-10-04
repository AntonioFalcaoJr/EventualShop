using ECommerce.WebAPI.IntegrationTests.Factories;
using Microsoft.AspNetCore.Mvc.Testing;
using WebAPI;
using Xunit;

namespace ECommerce.WebAPI.IntegrationTests;

public class ShoppingCartIntegrationTests : IClassFixture<TestingWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ShoppingCartIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }
}