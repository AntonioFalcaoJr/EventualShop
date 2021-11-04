using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Messages.JsonConverters;

public class ExpirationDateOnlyJsonConverter : JsonConverter
{
    public const string Format = "MM/yy";

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        => writer.WriteValue(((DateOnly)value).ToString(Format, CultureInfo.InvariantCulture));

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        => DateOnly.ParseExact(reader.Value as string ?? string.Empty, Format, CultureInfo.InvariantCulture);

    public override bool CanConvert(Type objectType)
        => objectType == typeof(DateOnly);
}