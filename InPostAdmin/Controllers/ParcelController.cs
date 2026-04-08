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

        return ExecuteWithValidation(
            action: () =>
            {
                var domainParcel = ParcelMapper.ToDomain(vm);
                _parcelService.Add(domainParcel);
            },
            successResult: RedirectToAction(nameof(Parcels), "Parcel"),
            errorResult: (msg) =>
            { 
                ModelState.AddModelError(string.Empty, msg);
                return View(vm);
            }
        );
    }

    public IActionResult Search(string trackingNumber)
    {
        if (string.IsNullOrWhiteSpace(trackingNumber)) return RedirectToAction(nameof(Parcels));


        return ExecuteQuery(
            query: () =>
            {
                var tn = new TrackingNumber(trackingNumber);
                return _parcelService.GetByNumber(tn);
            },
            successView: (domainResult) =>
            {
                var resultsList = new List<ParcelViewModel>();

                if (domainResult is not null) resultsList.Add(ParcelMapper.ToViewModel(domainResult));

                return View(nameof(Parcels), resultsList);
            },
            errorRedirectAction: nameof(Parcels)
        );
    }
}