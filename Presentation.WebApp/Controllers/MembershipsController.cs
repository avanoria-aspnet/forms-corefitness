using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Attributes.MenuNavigation;

namespace Presentation.WebApp.Controllers;

public class MembershipsController : Controller
{
    [MenuItem("Memberships", 1)]
    public IActionResult Index()
    {
        return View();
    }
}
