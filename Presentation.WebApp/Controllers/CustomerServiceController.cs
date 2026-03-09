using Application.CustomerService.ContactRequests;
using Application.CustomerService.ContactRequests.Inputs;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Attributes.MenuNavigation;
using Presentation.WebApp.Models.Forms;

namespace Presentation.WebApp.Controllers;

[Route("[controller]")]
public class CustomerServiceController(IContactRequestService crService) : Controller
{
    [HttpGet]
    [MenuItem("Customer Service", 4)]
    public IActionResult Index()
    {
        return View(new ContactForm());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ContactForm form, CancellationToken ct = default)
    {
        if (!ModelState.IsValid)
            return View(form);

        var input = new ContactRequestInput(
            form.FirstName, 
            form.LastName,
            form.Email,
            form.PhoneNumber,
            form.Message
        );

        var result = await crService.CreateContactRequestAsync(input, ct);
        
        TempData["ContactFormMessage"] = result.Success
            ? "Your message has been sent."
            : "Your message could not be sent. Please try again later.";

        return RedirectToAction(nameof(Index));
    }
}
