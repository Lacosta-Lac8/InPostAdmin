using System.Text.RegularExpressions;

namespace InPostAdmin.Common.Helpers;

public static class TrackingNumberHelper
{
    private const string DefaultPrefix = "PL";

    public static string NormalizeTracking(this string? number)
    {
        if (string.IsNullOrWhiteSpace(number)) return string.Empty;

        var normalized = number.Trim().ToUpper();

        if (Regex.IsMatch(normalized, @"^\d+$")) return DefaultPrefix + normalized;

        return normalized;
    }
}