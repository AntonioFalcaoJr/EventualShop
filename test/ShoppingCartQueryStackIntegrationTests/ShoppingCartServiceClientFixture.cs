using Contracts.Services.ShoppingCart.Protobuf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace ShoppingCartQueryStackIntegrationTests;

public class ShoppingCartServiceClientFixture : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            services
                .AddGrpcClient<ShoppingCartService.ShoppingCartServiceClient>(
                    options => options.Address = Server.BaseAddress)
                .ConfigureChannel(
                    options => options.HttpHandler = Server.CreateHandler());
        });
    }
}