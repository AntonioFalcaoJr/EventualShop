using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Contracts.JsonConverters;

public class DateOnlyTextJsonConverter : JsonConverter<DateOnly>
{
    private const string Format = "dd/MM/yyyy";

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => DateOnly.ParseExact(reader.GetString() ?? string.Empty, Format, CultureInfo.InvariantCulture);

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
}