using System.Text.Json;
using Contracts.JsonConverters;
using Domain.Abstractions.Aggregates;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EventStore.Contexts.Converters;

public class AggregateConverter : ValueConverter<IAggregateRoot?, string>
{
    public AggregateConverter()
        : base(
            @event => JsonSerializer.Serialize(@event, typeof(IAggregateRoot), SerializerOptions()),
            jsonString => JsonSerializer.Deserialize<IAggregateRoot>(jsonString, SerializerOptions())) { }

    private static JsonSerializerOptions SerializerOptions()
    {
        JsonSerializerOptions jsonSerializerOptions = new();
        jsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        jsonSerializerOptions.Converters.Add(new ExpirationDateOnlyJsonConverter());
        return jsonSerializerOptions;
    }
}