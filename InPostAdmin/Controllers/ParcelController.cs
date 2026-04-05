using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InPostAdmin.Models;
using InPostAdmin.Services;
using InPostAdmin.ViewModels;
using InPostAdmin.Interfaces;
using InPostAdmin.Mappers;

namespace InPostAdmin.Controllers;

public class ParcelController : BaseController
{
    private readonly IParcelService _parcelService;

    public ParcelController(IParcelService parcelService)
    {
        _parcelService = parcelService;
    }

    public IActionResult Parcels(ParcelStatus? status = null)
    {
        var domainParcels = _parcelService.GetParcels(status);
        var viewModels = ParcelMapper.ToViewModelList(domainParcels);
        return View(viewModels);
    }
    
    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public IActionResult Register(RegisterParcelViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        try
        {
            var newParcel = ParcelMapper.ToDomain(vm);

            _parcelService.Add(newParcel);
            return RedirectToAction(nameof(Parcels));
        }
        catch (Exception ex) when (ex is ArgumentException or KeyNotFoundException)
        {
            ModelState.AddModelError(nameof(vm.TrackingNumber), ex.Message);
            return View(vm);
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "A critical error occured while saving.");
            return View(vm);
        }
    }

    [HttpPost]
    public IActionResult DeleteParcel(Guid id)
    {
        return ExecuteWithNotification(() => _parcelService.Delete(id), "Parcel successfully removed from the system.",
            nameof(Parcels));
    }

    public IActionResult Search(string number)
    {
        if (string.IsNullOrWhiteSpace(number)) return RedirectToAction(nameof(Parcels));

        var trackingNumber = new TrackingNumber(number);
        var domainResult = _parcelService.GetByNumber(trackingNumber);
        List<ParcelViewModel> resultsList = new();

        if (domainResult is not null)
        {
            ParcelViewModel viewModel = ParcelMapper.ToViewModel(domainResult);
            resultsList.Add(viewModel);
        }

        return View(nameof(Parcels), resultsList);
    }

    [HttpPost]
    public IActionResult UpdateStatus(Guid id, ParcelStatus newStatus)
    {
        return ExecuteWithNotification(() => _parcelService.UpdateStatus(id, newStatus), "Parcel status has been updated.",
            nameof(Parcels));
    }
}