using Application.Abstractions;
using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Uri = System.Uri;

namespace Infrastructure.Projections.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddProjectionsInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(typeof(IProjectionGateway<>), typeof(ProjectionGateway<>));
        
        services.AddSingleton(provider =>
        {
            var connectionString = provider.GetRequiredService<IConfiguration>().GetConnectionString("Elasticsearch");
            var settings = new ElasticsearchClientSettings(new Uri(connectionString!)); 
            return new ElasticsearchClient(settings);
        });
    }
}