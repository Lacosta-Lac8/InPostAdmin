using InPostAdmin.Common.Helpers;
using InPostAdmin.Models;
using InPostAdmin.Interfaces;

namespace InPostAdmin.Services;

public class ParcelService : IParcelService
{
    private readonly IParcelRepository _repository;

    public ParcelService(IParcelRepository repository)
    {
        _repository = repository;
    }

    public List<Parcel> GetAll()
    {
        var result = _repository.GetAll();
        return result ?? new List<Parcel>();
    }

    public List<Parcel> GetParcelsByStatus(ParcelStatus status) => _repository.GetByStatus(status);
    
    public void Add(Parcel parcel)
    {
        if (parcel is null)
        {
            throw new ArgumentNullException(nameof(parcel), "The parcel object was not provided.");
        }
        if (string.IsNullOrWhiteSpace(parcel.TrackingNumber))
        {
            throw new ArgumentException("Tracking number cannot be empty or whitespace.", nameof(parcel.TrackingNumber));
        }

        _repository.Add(parcel);
    }

    public List<Parcel> GetParcels(ParcelStatus? status) => status.HasValue ? GetParcelsByStatus(status.Value) : GetAll();
    
    
    public void Delete(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Invalid ID provided.", nameof(id));
        }
        bool deleted = _repository.Delete(id);

        if (!deleted)
        {
            throw new KeyNotFoundException($"Cannot delete. Parcel with ID {id} not found.");
        }
    }
    public Parcel? GetByNumber(TrackingNumber number)
    {
        return _repository.GetByNumber(number);
    }
    
    public void UpdateStatus(Guid id, ParcelStatus newStatus)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Invalid ID provided.", nameof(id));
        }
        
        bool isUpdated = _repository.UpdateStatus(id, newStatus);
        if (!isUpdated)
        {
            throw new KeyNotFoundException($"Parcel with ID {id} was not found.");
        }
    }

    public void UpdateStatusesAutomatically()
    {
        var allParcel = _repository.GetAll();

        foreach (var parcel in allParcel)
        {
            var age = DateTime.Now - parcel.CreatedAt;
            var currentStatus = parcel.Status;

            if (currentStatus is ParcelStatus.Created && age > TimeSpan.FromMinutes(1))
            {
                _repository.UpdateStatus(parcel.Id, ParcelStatus.InTransit);
            }
            else if (currentStatus is ParcelStatus.InTransit && age > TimeSpan.FromMinutes(2))
            {
                _repository.UpdateStatus(parcel.Id, ParcelStatus.Ready);
            }
            else if (currentStatus is ParcelStatus.Ready && age > TimeSpan.FromMinutes(3))
            {
                _repository.UpdateStatus(parcel.Id, ParcelStatus.Delivered);
            }
        }
    }
}