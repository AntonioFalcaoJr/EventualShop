using System.Text.Json;
using Contracts.Abstractions.Messages;
using Contracts.JsonConverters;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EventStore.Contexts.Converters;

public class EventConverter : ValueConverter<IEvent?, string>
{
    public EventConverter()
        : base(
            @event => JsonSerializer.Serialize(@event, typeof(IEvent), SerializerOptions()),
            jsonString => JsonSerializer.Deserialize<IEvent>(jsonString, SerializerOptions()))
    {
    }

    private static JsonSerializerOptions SerializerOptions()
    {
        JsonSerializerOptions jsonSerializerOptions = new();
        jsonSerializerOptions.Converters.Add(new EventJsonConverter());
        jsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        jsonSerializerOptions.Converters.Add(new ExpirationDateOnlyJsonConverter());
        return jsonSerializerOptions;
    }
}