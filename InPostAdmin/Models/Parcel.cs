using System.Runtime.InteropServices.JavaScript;
using InPostAdmin.Models;
using System.ComponentModel.DataAnnotations;

namespace InPostAdmin.Models;

public enum ParcelStatus
{
    Created,
    InTransit,
    Ready,
    Delivered
}

public class Parcel
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime CreatedAt { get; } = DateTime.Now;
    public string TrackingNumber { get; set;  } = string.Empty;
    public string RecipientName { get; set; } = string.Empty;
    public ParcelStatus Status { get; set; } = ParcelStatus.Created;
}