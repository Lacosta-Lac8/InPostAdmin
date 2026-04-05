using InPostAdmin.Models;

namespace InPostAdmin.Common.Helpers;

public static class EnumExtensions
{
    public static (string Text, string Color) GetMetadata(this ParcelStatus status) => status switch
    {
        ParcelStatus.Created   => ("Oczekuje w systemie", "oklch(90% 0 0)"),
        ParcelStatus.Ready     => ("Gotowa do nadaia", "oklch(85% 0.1 60"),
        ParcelStatus.InTransit => ("W trasie do paczkomatu", "oklch(70% 0.2 200)"),
        ParcelStatus.Delivered => ("Dostarczona", "oklch(75% 0.2 145)"),
        _                      => ("Nieznany", "oklch(90% 0 0)")
    };

    public static string ToPolichString(this ParcelStatus status) => status.GetMetadata().Text;
    public static string GetColor(this ParcelStatus status) => status.GetMetadata().Color;
}