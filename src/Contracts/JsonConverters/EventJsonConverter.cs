using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Contracts.Abstractions.Messages;

namespace Contracts.JsonConverters;

public class EventJsonConverter : JsonConverter<IEvent?>
{
    private const string DiscriminatorFieldName = "$type";

    private static readonly IEnumerable<Assembly> SecuredAssemblies = new[] { typeof(IMessage).Assembly };

    private static readonly IEnumerable<Type> EventTypes = SecuredAssemblies
        .SelectMany(x => x.GetTypes())
        .Where(x => x.IsAssignableTo(typeof(IEvent)))
        .Where(x => !x.IsAbstract);

    private static readonly IDictionary<string, Type> EventTypesByDiscriminator = EventTypes.ToDictionary(GetDiscriminator, x => x);
    
    private static readonly IDictionary<Type, string> EventDiscriminatorByTypes = EventTypes.ToDictionary(x => x, GetDiscriminator);

    private static string GetDiscriminator(Type type) 
        => $"{type.Namespace}.{type.Name}";

    public override IEvent? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject) throw new JsonException();

        using var jsonDocument = JsonDocument.ParseValue(ref reader);
        if (!jsonDocument.RootElement.TryGetProperty(DiscriminatorFieldName, out var discriminatorJsonElement)) throw new JsonException();

        var discriminator = discriminatorJsonElement.GetString() ?? string.Empty;
        if (!EventTypesByDiscriminator.TryGetValue(discriminator, out var type)) throw new JsonException();

        var jsonObject = jsonDocument.RootElement.GetRawText();
        return (IEvent) JsonSerializer.Deserialize(jsonObject, type, options)!;
    }

    public override void Write(Utf8JsonWriter writer, IEvent? value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteNullValue();
            return;            
        }

        var type = value.GetType();
        if (!EventDiscriminatorByTypes.TryGetValue(type, out var discriminator)) throw new JsonException();

        var nativeSerialization = JsonSerializer.SerializeToNode(value, type, options)!;
        if (nativeSerialization is JsonArray) throw new JsonException();
        
        var jsonObject = new JsonObject(GetPropertiesWithDiscriminator(discriminator, nativeSerialization));
        jsonObject.WriteTo(writer, options);
    }

    private IEnumerable<KeyValuePair<string, JsonNode?>> GetPropertiesWithDiscriminator(string discriminator, JsonNode nativeSerialization)
    {
        if (nativeSerialization is not JsonObject nativeJsonObject) yield break;

        yield return new KeyValuePair<string, JsonNode?>(DiscriminatorFieldName, discriminator);
        foreach (var property in GetProperties(nativeJsonObject))
            yield return new KeyValuePair<string, JsonNode?>(property.Key, GetPropertyValue(property.Value));
    }

    private JsonNode? GetPropertyValue(JsonNode? jsonNode) 
        => jsonNode switch
        {
            JsonValue jsonValue => JsonNode.Parse(jsonValue.ToJsonString()) as JsonValue,
            JsonObject jsonObject => new JsonObject(GetProperties(jsonObject)),
            JsonArray jsonArray => new JsonArray(GetProperties(jsonArray).ToArray()),
            _ => null
        };

    private IDictionary<string, JsonNode?> GetProperties(JsonObject jsonObject)
        => jsonObject.ToDictionary(x => x.Key, x => GetPropertyValue(x.Value));

    private IEnumerable<JsonNode?> GetProperties(JsonArray jsonArray)
        => jsonArray.Select(GetPropertyValue);
}