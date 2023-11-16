using Serilog;

namespace WorkerService.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder ConfigureLogging(this IHostBuilder builder)
        => builder
            .ConfigureLogging(logging => logging.ClearProviders().AddSerilog())
            .UseSerilog((context, cfg) => cfg.ReadFrom.Configuration(context.Configuration));

    public static IHostBuilder ConfigureServiceProvider(this IHostBuilder builder)
        => builder.UseDefaultServiceProvider((context, provider)
            => provider.ValidateScopes =
                provider.ValidateOnBuild =
                    context.HostingEnvironment.IsDevelopment());

    public static IHostBuilder ConfigureAppConfiguration(this IHostBuilder builder)
        => builder.ConfigureAppConfiguration(configuration
            => configuration
                .AddUserSecrets<Program>()
                .AddEnvironmentVariables());
}