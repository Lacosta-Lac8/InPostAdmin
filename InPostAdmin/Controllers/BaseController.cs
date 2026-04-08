using InPostAdmin.Common;
using Microsoft.AspNetCore.Mvc;

namespace InPostAdmin.Controllers;

public abstract class BaseController : Controller
{
    protected IActionResult ExecuteWithNotification(Action action, string successMessage, string actionName, string controllerName = null)
    {
        try
        {
            action();
            TempData[WebConstants.SuccessMessage] = successMessage;
        }
        catch (Exception ex) when (ex is ArgumentException or KeyNotFoundException)
        {
            TempData[WebConstants.ErrorMessage] = ex.Message;
        }
        catch (Exception)
        {
            TempData[WebConstants.ErrorMessage] = "A critical system error occurred. Please contact support.";
        }

        return RedirectToAction(actionName, controllerName);
    }

    protected IActionResult ExecuteQuery<T>(Func<T> query, Func<T, IActionResult> successView,
        string errorRedirectAction, string errorRedirectController = null)
    {
        try
        {
            var result = query();
            return successView(result);
        }
        catch (Exception ex) when (ex is ArgumentException or KeyNotFoundException)
        {
            TempData[WebConstants.ErrorMessage] = ex.Message;
            return RedirectToAction(errorRedirectAction, errorRedirectController);
        }
        catch (Exception)
        {
            TempData[WebConstants.ErrorMessage] = "A critical system error occurred.";
            return RedirectToAction(errorRedirectAction, errorRedirectController);
        }
    }

    protected IActionResult ExecuteWithValidation(Action action, IActionResult successResult,
        Func<string, IActionResult> errorResult)
    {
        try
        {
            action();
            return successResult;
        }
        catch (Exception ex) when (ex is ArgumentException or KeyNotFoundException)
        {
            return errorResult(ex.Message);
        }
        catch (Exception)
        {
            return errorResult("Wystąpił ktytyczny błąd systemu");
        }
    }
}