using Application.Abstractions.Projections;
using Infrastructure.Projections.Abstractions.Contexts;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Infrastructure.Projections.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddProjections(this IServiceCollection services)
    {
        services.AddScoped(typeof(IProjectionRepository<>), typeof(ProjectionRepository<>));
        services.AddScoped<IMongoDbContext, ProjectionDbContext>();
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.CSharpLegacy));
    }
}