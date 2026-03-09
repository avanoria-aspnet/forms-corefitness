using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Attributes.MenuNavigation;

namespace Presentation.WebApp.Controllers;

[Route("[controller]")]
public class TrainingController : Controller
{
    [Route("personal-training")]

    [MenuItem("Personal Training", 1)]
    public IActionResult PersonalTraining()
    {
        return View();
    }

    [Route("online-coaching")]
    [MenuItem("Online Coaching", 2)]
    public IActionResult OnlineCoaching()
    {
        return View();
    }

    [Route("group-training")]
    [MenuItem("Group Training", 3)]
    public IActionResult GroupTraining()
    {
        return View();
    }
}
