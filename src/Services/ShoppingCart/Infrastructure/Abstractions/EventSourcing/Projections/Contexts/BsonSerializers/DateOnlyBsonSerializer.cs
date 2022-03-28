using System.Globalization;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Infrastructure.Abstractions.EventSourcing.Projections.Contexts.BsonSerializers;

public class DateOnlyBsonSerializer : SerializerBase<DateOnly>
{
    private const string Format = "dd/MM/yyyy";

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateOnly value)
        => context.Writer.WriteString(value.ToString(Format, CultureInfo.InvariantCulture));

    public override DateOnly Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        => DateOnly.ParseExact(context.Reader.ReadString(), Format, CultureInfo.InvariantCulture);
}