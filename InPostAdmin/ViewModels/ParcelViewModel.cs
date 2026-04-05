using InPostAdmin.Models;
namespace InPostAdmin.ViewModels;

public class ParcelViewModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string TrackingNumber { get; set; } = string.Empty;
    public string RecipientName { get; set; } = string.Empty;
    public string StatusColor { get; set; } = string.Empty;
    public string DisplayStatus { get; set; }
    public string SystemStatus { get; set; }
}