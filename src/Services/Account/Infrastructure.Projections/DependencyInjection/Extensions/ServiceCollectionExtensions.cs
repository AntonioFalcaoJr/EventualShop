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
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services.AddScoped<IAccountProjectionsService, AccountProjectionsService>();

    public static IServiceCollection AddProjectionsDbContext(this IServiceCollection services)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.CSharpLegacy));
        return services.AddScoped<IMongoDbContext, ProjectionsDbContext>();
    }

    public static IServiceCollection AddProjectionsRepository(this IServiceCollection services)
        => services.AddScoped<IAccountProjectionsRepository, AccountProjectionsRepository>();
}