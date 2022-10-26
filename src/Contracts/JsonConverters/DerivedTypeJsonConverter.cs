using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Contracts.JsonConverters;

public class DerivedTypeJsonConverter<T> : JsonConverter<T?>
{
    private const string DiscriminatorFieldName = "$type";

    protected virtual IEnumerable<Assembly> SecuredAssemblies => new[] { typeof(T).Assembly };

    private IEnumerable<(Type Type, string Discriminator)> Types => SecuredAssemblies
        .SelectMany(x => x.GetTypes())
        .Where(x => x.IsAssignableTo(typeof(T)))
        .Where(x => !x.IsAbstract)
        .Select(x => (x, GetDiscriminator(x)))
        .ToArray();

    private string GetDiscriminator(Type type)
        => $"{type.Namespace}.{type.Name}";

    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject) throw new JsonException();

        using var jsonDocument = JsonDocument.ParseValue(ref reader);
        if (!jsonDocument.RootElement.TryGetProperty(DiscriminatorFieldName, out var discriminatorJsonElement)) throw new JsonException();

        var discriminator = discriminatorJsonElement.GetString() ?? string.Empty;
        var type = Types.First(x => x.Discriminator == discriminator).Type;

        var jsonObject = jsonDocument.RootElement.GetRawText();
        return (T?)JsonSerializer.Deserialize(jsonObject, type, options)!;
    }

    public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteNullValue();
            return;
        }

        var type = value.GetType();
        var discriminator = Types.First(x => x.Type == type).Discriminator;
        var node = JsonSerializer.SerializeToNode(value, type, options)!;
        node[DiscriminatorFieldName] = discriminator;
        node.WriteTo(writer, options);
    }
}