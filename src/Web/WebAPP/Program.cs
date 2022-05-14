using System.Reflection;
using BlazorStrap;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Serilog;
using WebAPP;
using WebAPP.DependencyInjection;
using WebAPP.DependencyInjection.Options;
using WebAPP.ViewModels;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.ConfigureContainer(new DefaultServiceProviderFactory(new()
{
    ValidateScopes = builder.HostEnvironment.IsDevelopment(),
    ValidateOnBuild = true
}));

builder.Configuration
    .AddUserSecrets(Assembly.GetExecutingAssembly())
    .AddEnvironmentVariables();

Log.Logger = new LoggerConfiguration().ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

builder.Services.AddECommerceHttpClient();
builder.Services.AddBlazorStrap();

builder.Services.AddScoped<CatalogsViewModel>();
builder.Services.AddScoped<CatalogItemViewModel>();

builder.Services.ConfigureECommerceHttpClientOptions(
    builder.Configuration.GetSection(nameof(ECommerceHttpClientOptions)));

await builder.Build().RunAsync();