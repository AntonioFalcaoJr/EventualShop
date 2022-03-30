using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ECommerce.WebAPI.IntegrationTests.Factories;

public class TestingWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> 
    where TEntryPoint : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.Where(descriptor => descriptor.ServiceType.Assembly.FullName?.Contains(nameof(MassTransit), StringComparison.OrdinalIgnoreCase) ?? default).ToList()
                .ForEach(descriptor => services.Remove(descriptor));

            services.AddMassTransit(configurator => configurator.UsingInMemory());
        });
    }
}