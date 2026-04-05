using InPostAdmin.Models;
namespace InPostAdmin.Interfaces;

public interface IParcelRepository
{
    List<Parcel> GetAll();
    List<Parcel> GetByStatus(ParcelStatus status);
    Parcel? GetById(Guid id);
    Parcel? GetByNumber(TrackingNumber number);
    
    void Add(Parcel parcel);
    bool Delete(Guid id);
    bool UpdateStatus(Guid id, ParcelStatus newStatus);
}