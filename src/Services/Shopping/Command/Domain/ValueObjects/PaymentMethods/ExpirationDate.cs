using System.Text.RegularExpressions;
using static Domain.Exceptions;

namespace Domain.ValueObjects.PaymentMethods;

public record ExpirationDate
{
    private readonly ushort _month;
    private readonly ushort _year;

    public ExpirationDate(ushort month, ushort year)
    {
        InvalidMonth.ThrowIf(month is < 1 or > 12);
        InvalidYear.ThrowIf(year < DateTime.UtcNow.Year || year > DateTime.UtcNow.Year + 10);

        _month = month;
        _year = year;
    }

    public static bool TryParse(string input, out ExpirationDate expiration)
    {
        expiration = null!;

        if (Regex.IsMatch(input, @"^\d{2}/\d{2}$"))
        {
            var month = ushort.Parse(input[..2]);
            var year = ushort.Parse(input[3..]);
            expiration = new(month, year);
            return true;
        }

        return false;
    }

    public static implicit operator ExpirationDate(string input)
        => TryParse(input, out var expiration) ? expiration : throw new ArgumentException("Invalid expiration date format.");

    public static implicit operator string(ExpirationDate expiration) => $"{expiration._month:D2}/{expiration._year:D2}";
}