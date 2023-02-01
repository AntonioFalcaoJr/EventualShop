using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ShoppingCartQueryStackIntegrationTests;

public class TestingWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> 
    where TEntryPoint : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
        => builder.UseEnvironment("Testing");
}