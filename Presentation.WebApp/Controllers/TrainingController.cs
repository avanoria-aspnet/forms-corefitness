using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Attributes.MenuNavigation;

namespace Presentation.WebApp.Controllers;

public class TrainingController : Controller
{
    [MenuItem("Personal Training", 1)]
    public IActionResult PersonalTraining()
    {
        return View();
    }

    [MenuItem("Online Coaching", 2)]
    public IActionResult OnlineCoaching()
    {
        return View();
    }

    [MenuItem("Group Training", 3)]
    public IActionResult GroupTraining()
    {
        return View();
    }
}
