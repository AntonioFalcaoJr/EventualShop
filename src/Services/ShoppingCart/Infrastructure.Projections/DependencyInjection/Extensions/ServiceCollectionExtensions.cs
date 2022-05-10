using Application.Abstractions.Projections;
using Contracts.DataTransferObjects;
using Infrastructure.Projections.Abstractions.Contexts;
using Infrastructure.Projections.Abstractions.Contexts.BsonSerializers;
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

        BsonSerializer.RegisterSerializer(new DateOnlyBsonSerializer());
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.CSharpLegacy));
        BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
        BsonSerializer.RegisterSerializer(typeof(decimal?), new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));

        // TODO - Review this
        BsonClassMap.RegisterClassMap<Dto.CreditCard>();
        BsonClassMap.RegisterClassMap<Dto.DebitCard>();
        BsonClassMap.RegisterClassMap<Dto.PayPal>();
    }
}