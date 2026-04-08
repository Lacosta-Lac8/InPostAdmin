using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InPostAdmin.Models;
using InPostAdmin.Services;
using InPostAdmin.ViewModels;
using InPostAdmin.Interfaces;
using InPostAdmin.Mappers;
using Microsoft.AspNetCore.Authorization;

namespace InPostAdmin.Controllers;

[Authorize(Roles = "Administrator")]
public class AdminController : BaseController
{
    private readonly IParcelService _parcelService;
    public AdminController(IParcelService parcelService) => _parcelService = parcelService;
    
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
}