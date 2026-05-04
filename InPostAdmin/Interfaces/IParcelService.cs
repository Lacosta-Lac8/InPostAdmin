namespace InPostAdmin.Interfaces;

public interface IParcelService
{
    
    List<Parcel> GetAll();
    List<Parcel> GetParcelsByStatus(ParcelStatus status);
    List<Parcel> GetParcels(ParcelStatus? status);
    Parcel? GetByNumber(TrackingNumber number);
    
    void Add(Parcel parcel);
    void Delete(Guid id);
    void UpdateStatus(Guid id, ParcelStatus newStatus);
    void UpdateStatusesAutomatically();
}