using Application.EventSourcing.Projections;
using Infrastructure.Projections.Abstractions.Contexts;
using Infrastructure.Projections.Contexts;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Infrastructure.Projections.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddProjections(this IServiceCollection services)
    {
        services.AddScoped<IWarehouseProjectionsService, WarehouseProjectionsService>();
        services.AddScoped<IWarehouseProjectionsRepository, WarehouseProjectionsRepository>();
        services.AddScoped<IMongoDbContext, ProjectionsDbContext>();
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.CSharpLegacy));
    }
}