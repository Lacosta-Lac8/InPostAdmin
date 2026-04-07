using System.Text.RegularExpressions;

namespace InPostAdmin.Models;

public readonly struct TrackingNumber
{
    private readonly string _value;

    public TrackingNumber(string rawNumber)
    {
        if (string.IsNullOrWhiteSpace(rawNumber)) throw new ArgumentException("Numer przesyłki nie może być pusty.");

        var normalized = rawNumber.Trim().ToUpper();
        if (Regex.IsMatch(normalized, @"^\d+$") || !normalized.StartsWith("PL"))
        {
            normalized = "PL" + normalized;
        }
        _value = normalized;
    }

    public override string ToString() => _value;
    public static implicit operator string(TrackingNumber tn) => tn._value;
}