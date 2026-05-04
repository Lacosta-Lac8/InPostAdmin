namespace InPostAdmin.Repositories;

public class ParcelRepository : IParcelRepository
{
    private static readonly List<Parcel> _parcels = new();
    private readonly object _serviceLock = new();

    public List<Parcel> GetAll()
    {
        lock (_serviceLock) return _parcels.ToList();
    }

    public List<Parcel> GetByStatus(ParcelStatus status)
    {
        lock (_serviceLock)
        {
            return _parcels.Where(p => p.Status == status).ToList();
        }
    }

    public void Add(Parcel parcel)
    {
        lock (_serviceLock)
        {
            Console.WriteLine($">>> [REPO] {DateTime.Now:HH:mm:ss} | Added: {parcel.TrackingNumber} (ID: {parcel.Id})");
            _parcels.Add(parcel);
        }
    }

    public bool Delete(Guid id)
    {
        lock (_serviceLock)
        {
            var parcel = _parcels.FirstOrDefault(p => p.Id == id);

            if (parcel is null)
            {
                Console.WriteLine($"!!! [REPO] {DateTime.Now:HH:mm:ss} | WARNING: Delete failed. ID {id} not found.");
                return false;
            }
            _parcels.Remove(parcel);
            Console.WriteLine($">>> [REPO] {DateTime.Now:HH:mm:ss} | SUCCESS: Deleted ID {id}");
            return true;
        }
    }

    public Parcel? GetByNumber(TrackingNumber number)
    {
        lock (_serviceLock)
        {
            Console.WriteLine($">>> [REPO] {DateTime.Now:HH:mm:ss} | Searching for: {number}");
            return _parcels.FirstOrDefault(p => p.TrackingNumber == number);
        }
    }

    public bool UpdateStatus(Guid id, ParcelStatus newStatus)
    {
        lock (_serviceLock)
        {
            var parcel = _parcels.FirstOrDefault(p => p.Id == id);
            if (parcel is null)
            {
                Console.WriteLine($">>> [REPO] {DateTime.Now:HH:mm:ss} | WARNING: Update failed. ID {id} not found.");
                return false;
            }
            parcel.Status = newStatus;
            Console.WriteLine($">>> [REPO] {DateTime.Now:HH:mm:ss} | SUCCESS: Parcel {id} status changed to {newStatus}");
            return true;
        }
    }
    
    public Parcel? GetById(Guid id)
    {
        lock (_serviceLock) return _parcels.FirstOrDefault(p => p.Id == id);
    }
}