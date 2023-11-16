using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Version = Domain.ValueObjects.Version;

namespace Infrastructure.EventStore.Contexts.Converters;

public class VersionConverter()
    : ValueConverter<Version, uint>(version => version, number => Version.Number(number));