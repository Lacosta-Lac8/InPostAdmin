using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using InPostAdmin.Interfaces;

namespace InPostAdmin.Services;

public class ParcelStatusWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(1);

    public ParcelStatusWorker(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine(">>> ParcelStatusWorder STARTED");
        
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var parcelService = scope.ServiceProvider.GetRequiredService<IParcelService>();
                    
                    Console.WriteLine($">>> Checking statuses at {DateTime.Now}");
                    parcelService.UpdateStatusesAutomatically();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($">>> ERROR in Worker: {ex.Message}");
            }

            await Task.Delay(_checkInterval, stoppingToken);
        }
        Console.WriteLine(">>> ParcelStatusWorker STOPPED");
    }
}