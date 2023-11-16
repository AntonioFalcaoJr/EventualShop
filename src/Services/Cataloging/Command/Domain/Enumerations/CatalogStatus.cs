using Ardalis.SmartEnum;

namespace Domain.Enumerations;

public class CatalogStatus(string name, int value) : SmartEnum<CatalogStatus>(name, value)
{
    public static readonly CatalogStatusEmpty Empty = new();
    public static readonly CatalogStatusActive Active = new();
    public static readonly CatalogStatusInactive Inactive = new();
    public static readonly CatalogStatusDiscarded Discarded = new();

    public static explicit operator CatalogStatus(int value) => FromValue(value);
    public static explicit operator CatalogStatus(string name) => FromName(name);
    public static implicit operator int(CatalogStatus catalogStatus) => catalogStatus.Value;
    public static implicit operator string(CatalogStatus catalogStatus) => catalogStatus.Name;
    public override string ToString() => Name;
}

public class CatalogStatusEmpty() : CatalogStatus(nameof(Empty), 0);
public class CatalogStatusActive() : CatalogStatus(nameof(Active), 1);
public class CatalogStatusInactive() : CatalogStatus(nameof(Inactive), 2);
public class CatalogStatusDiscarded() : CatalogStatus(nameof(Discarded), 3);