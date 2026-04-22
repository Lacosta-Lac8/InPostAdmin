using InPostAdmin.Models;
using InPostAdmin.ViewModels;
using InPostAdmin.Common.Helpers;
namespace InPostAdmin.Mappers;

public class ParcelMapper
{
    public static ParcelViewModel ToViewModel(Parcel domain)
    {
        var metadata = domain.Status.GetMetadata();
        
        return new ParcelViewModel
        {
            Id = domain.Id,
            CreatedAt = domain.CreatedAt,
            TrackingNumber = domain.TrackingNumber,
            RecipientName = domain.RecipientName,
            DisplayStatus = metadata.Text,
            SystemStatus = domain.Status.ToString(),
            StatusColor = metadata.Color
        };
    }

    public static List<ParcelViewModel> ToViewModelList(IEnumerable<Parcel> domains) => domains.Select(p => ToViewModel(p)).ToList();

    public static Parcel ToDomain(RegisterParcelViewModel vm)
    {
        return new Parcel
        {
            TrackingNumber = new TrackingNumber(vm.TrackingNumber),
            RecipientName = vm.RecipientName,
            Status = vm.Status
        };
    }
}