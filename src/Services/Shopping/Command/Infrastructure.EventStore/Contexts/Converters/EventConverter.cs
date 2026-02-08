using Contracts.Abstractions.Messages;
using Contracts.JsonConverters;
using Domain.ValueObjects;
using JsonNet.ContractResolvers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Infrastructure.EventStore.Contexts.Converters;

public class EventConverter()
    : ValueConverter<IDomainEvent?, string>(
        @event => JsonConvert.SerializeObject(@event, typeof(IDomainEvent), SerializerSettings()),
        jsonString => JsonConvert.DeserializeObject<IDomainEvent>(jsonString, DeserializerSettings()))
{
    private static JsonSerializerSettings SerializerSettings()
    {
        JsonSerializerSettings jsonSerializerSettings = new()
        {
            TypeNameHandling = TypeNameHandling.Auto
        };

        jsonSerializerSettings.Converters.Add(new DateOnlyJsonConverter());
        jsonSerializerSettings.Converters.Add(new ExpirationDateOnlyJsonConverter());
        jsonSerializerSettings.Converters.Add(new CurrencyJsonConverter());

        return jsonSerializerSettings;
    }

    private static JsonSerializerSettings DeserializerSettings()
    {
        JsonSerializerSettings jsonDeserializerSettings = new()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            ContractResolver = new PrivateSetterContractResolver()
        };

        jsonDeserializerSettings.Converters.Add(new DateOnlyJsonConverter());
        jsonDeserializerSettings.Converters.Add(new ExpirationDateOnlyJsonConverter());
        jsonDeserializerSettings.Converters.Add(new CurrencyJsonConverter());

        return jsonDeserializerSettings;
    }
}

public class CurrencyJsonConverter : JsonConverter<Currency>
{
    public override void WriteJson(JsonWriter writer, Currency? value, JsonSerializer serializer)
        => writer.WriteValue(value?.IsoCode ?? string.Empty);

    public override Currency ReadJson(JsonReader reader, Type objectType, Currency? existingValue, bool hasExistingValue, JsonSerializer serializer)
        => (Currency)(reader.Value as string ?? string.Empty);
}