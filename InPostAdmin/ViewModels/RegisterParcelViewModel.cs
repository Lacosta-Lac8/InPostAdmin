using System.ComponentModel.DataAnnotations;
using InPostAdmin.Models;

namespace InPostAdmin.ViewModels;

public class RegisterParcelViewModel
{
    [Required(ErrorMessage = "Numer przesyłki jest wymagany!")]
    [StringLength(12, MinimumLength = 8, ErrorMessage = "Liczba musi mieć od 8 do 12 znaków.")]
    public string TrackingNumber { get; set;  } = string.Empty;
    
    [Required(ErrorMessage = "Podanie nazwy odbiorcy jest wymagane.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Nazwa jest za krótka.")]
    public string RecipientName { get; set; } = string.Empty;
    
    public ParcelStatus Status { get; set; } = ParcelStatus.Created;
}