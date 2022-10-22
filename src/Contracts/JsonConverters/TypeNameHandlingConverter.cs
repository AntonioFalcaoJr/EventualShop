using Newtonsoft.Json;

namespace Contracts.JsonConverters;

public class TypeNameHandlingConverter : JsonConverter
{
    private readonly TypeNameHandling _typeNameHandling;

    public TypeNameHandlingConverter(TypeNameHandling typeNameHandling)
    {
        _typeNameHandling = typeNameHandling;
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        => new JsonSerializer { TypeNameHandling = _typeNameHandling }.Serialize(writer, value);

    public override object? ReadJson(JsonReader reader, Type type, object? existingValue, JsonSerializer serializer)
        => new JsonSerializer { TypeNameHandling = _typeNameHandling }.Deserialize(reader, type);

    public override bool CanConvert(Type type)
        => IsMassTransitOrSystemType(type) is false;

    private static bool IsMassTransitOrSystemType(Type type)
        => (type.Assembly.FullName?.Contains(nameof(MassTransit)) ?? false) ||
           type.Assembly.IsDynamic ||
           type.Assembly == typeof(object).Assembly;
}