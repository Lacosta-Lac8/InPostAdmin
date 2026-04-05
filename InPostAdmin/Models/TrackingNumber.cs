namespace InPostAdmin.Models;

public readonly struct TrackingNumber
{
    private readonly string _value;

    public TrackingNumber(string rawNumber)
    {
        if (string.IsNullOrWhiteSpace(rawNumber)) throw new ArgumentException("Numer przesyłki nie może być pusty.");

        var processed = rawNumber.Trim().ToUpper();
        if (!processed.StartsWith("PL")) processed = "PL" + processed;
        _value = processed;
    }

    public override string ToString() => _value;
    public static implicit operator string(TrackingNumber tn) => tn._value;
}