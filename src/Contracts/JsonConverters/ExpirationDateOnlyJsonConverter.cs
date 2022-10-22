using System.Globalization;
using Newtonsoft.Json;

namespace Contracts.JsonConverters;

public class ExpirationDateOnlyJsonConverter : JsonConverter
{
    private const string Format = "MM/yy";

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        => writer.WriteValue((value is DateOnly only ? only : default).ToString(Format, CultureInfo.InvariantCulture));

    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        => DateOnly.ParseExact(reader.Value as string ?? string.Empty, Format, CultureInfo.InvariantCulture);

    public override bool CanConvert(Type objectType)
        => objectType == typeof(DateOnly);
}