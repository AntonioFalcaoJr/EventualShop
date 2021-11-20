using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates;
using JsonNet.ContractResolvers;
using Messages.JsonConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Infrastructure.EventSourcing.EventStore.Configurations;

public class ShoppingCartSnapshotConfiguration : IEntityTypeConfiguration<ShoppingCartSnapshot>
{
    public void Configure(EntityTypeBuilder<ShoppingCartSnapshot> builder)
    {
        builder.HasKey(snapshot => new { snapshot.AggregateVersion, snapshot.AggregateId });

        builder
            .Property(snapshot => snapshot.AggregateVersion)
            .IsRequired();

        builder
            .Property(snapshot => snapshot.AggregateId)
            .IsRequired();

        builder
            .Property(snapshot => snapshot.AggregateName)
            .HasMaxLength(30)
            .IsUnicode(false)
            .IsRequired();

        builder
            .Property(snapshot => snapshot.AggregateState)
            .IsUnicode(false)
            .HasConversion(
                cart => JsonConvert.SerializeObject(cart, SerializerSettings()),
                jsonString => JsonConvert.DeserializeObject<Cart>(jsonString, DeserializerSettings()))
            .IsRequired();
    }

    private static JsonSerializerSettings SerializerSettings()
    {
        var jsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        };
        
        jsonSerializerSettings.Converters.Add(new DateOnlyJsonConverter());
        jsonSerializerSettings.Converters.Add(new ExpirationDateOnlyJsonConverter());
        
        return jsonSerializerSettings;
    }

    private static JsonSerializerSettings DeserializerSettings()
    {
        var jsonDeserializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            ContractResolver = new PrivateSetterContractResolver()
        };
        
        jsonDeserializerSettings.Converters.Add(new DateOnlyJsonConverter());
        jsonDeserializerSettings.Converters.Add(new ExpirationDateOnlyJsonConverter());

        return jsonDeserializerSettings;
    }
}