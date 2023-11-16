using Contracts.Shopping.Queries;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace ShoppingCartQueryStackIntegrationTests;

public class ShoppingCartServiceClientFixture : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            services
                .AddGrpcClient<ShoppingQueryService.ShoppingQueryServiceClient>(
                    options => options.Address = Server.BaseAddress)
                .ConfigureChannel(
                    options => options.HttpHandler = Server.CreateHandler());
        });
    }
}