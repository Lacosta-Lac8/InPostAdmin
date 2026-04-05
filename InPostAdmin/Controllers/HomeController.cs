using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InPostAdmin.Models;
using InPostAdmin.Services;
using InPostAdmin.ViewModels;
using InPostAdmin.Interfaces;
using InPostAdmin.Mappers;

namespace InPostAdmin.Controllers;

public class HomeController : BaseController
{
    public IActionResult Index() => View();
    
    private readonly IParcelService _parcelService;

    public HomeController(IParcelService parcelService)
    {
        _parcelService = parcelService;
    }
    
    public IActionResult Support() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}