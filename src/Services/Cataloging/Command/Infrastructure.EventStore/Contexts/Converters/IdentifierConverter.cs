using Domain.Abstractions.Identities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EventStore.Contexts.Converters;

public class IdentifierConverter<TId>() :
    ValueConverter<TId, Guid>(
        identifier => identifier,
        value => new() { Value = value })
    where TId : GuidIdentifier, new();