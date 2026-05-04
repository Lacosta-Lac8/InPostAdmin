using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InPostAdmin.Models;
using InPostAdmin.Services;
using InPostAdmin.ViewModels;
using InPostAdmin.Interfaces;
using InPostAdmin.Mappers;
using Microsoft.AspNetCore.Authorization;

namespace InPostAdmin.Controllers;

[Authorize(Policy = "AdminOnly")]
public class AdminController : BaseController
{
    private readonly IParcelService _parcelService;
    private readonly ILogger<AdminController> _logger;
    public AdminController(IParcelService parcelService) => _parcelService = parcelService;
    public AdminController(ILogger<AdminController> logger) => _logger = logger;
    
    [HttpPost]
    public IActionResult DeleteParcel(Guid id)
    {
        return ExecuteWithNotification(
            action: () => _parcelService.Delete(id),
            "Parcel successfully removed from the system.",
            actionName: "Parcels",
            controllerName: "Parcel"
        );
    }
    
    [HttpPost]
    public IActionResult UpdateStatus(Guid id, ParcelStatus newStatus)
    {
        return ExecuteWithNotification(
            action: () => _parcelService.UpdateStatus(id, newStatus),
            "Parcel status has been updated.",
            actionName: "Parcels",
            controllerName: "Parcel"
        );
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    public IActionResult CreateParcel(Parcel model)
    {
        _logger.LogInformation("Action: CreateParcel. User: {User}. Tracking: {Number}",
            User.Identity.Name, model.TrackingNumber);
        return Ok();
    }
}