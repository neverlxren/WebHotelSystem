using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebHotelSystem.Data;
using WebHotelSystem.Models;
using WebHotelSystem.ViewModels;

namespace WebHotelSystem.Controllers;

public class ClientController : Controller
{
    private readonly HotelDbContext _dbContext;

    public ClientController(HotelDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    // GET
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (_dbContext.Clients.Any(client => client.PhoneNumber == model.PhoneNumber))
        {
            ModelState.AddModelError("Phone number", "Phone number is alreaty taken");
            return View(model);
        }

        var client = new Client()
        {
            ClientName = model.ClientName,
            PhoneNumber = model.PhoneNumber,
            Email = model.Email,
            RegStatus = "registred",
            GuestCount = model.GuestCount,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        _dbContext.Clients.Add(client);
        await _dbContext.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    
    // http methods: get, post, put, patch, delete, query(get+post)
    
    
}