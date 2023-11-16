using Microsoft.AspNetCore.Mvc.Testing;
using WebAPI;
using Xunit;

namespace ECommerce.WebAPI.IntegrationTests;

public class ShoppingCartIntegrationTests(WebApplicationFactory<Program> factory) : IClassFixture<TestingWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();
}