using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Attributes.MenuNavigation;

namespace Presentation.WebApp.Controllers;

[Route("[controller]")]
public class CustomerServiceController : Controller
{
    [MenuItem("Customer Service", 4)]
    public IActionResult Index()
    {
        return View();
    }
}
