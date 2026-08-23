using Microsoft.AspNetCore.Mvc;

namespace WebHotelSystem.Controllers;

public class ApartmentController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}