using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebHotelSystem.Data;
using WebHotelSystem.Models;
using WebHotelSystem.ViewModels;

namespace WebHotelSystem.Controllers;

public class ApartmentController : Controller
{

    private readonly HotelDbContext _dbContext;

    public ApartmentController(HotelDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET
    [HttpGet]
    public async Task<IActionResult> ApartmentList()
    {
        var apartments = await _dbContext.Apartments
            .ToListAsync();

        return View(apartments);
    }


    [HttpGet]
    public async Task<IActionResult> ApartmentDetails(int id)
    {
        var apartment = await _dbContext.Apartments
            .FirstOrDefaultAsync(a => a.Id == id);

        if (apartment == null)
        {
            return NotFound();
        }

        return View(apartment);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ApartmentCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (await _dbContext.Apartments.AnyAsync(a => a.AppNumber == model.AppNumber))
        {
            ModelState.AddModelError(nameof(model.AppNumber), "Apartment with this number already exists");
            return View(model);
        }

        var apartment = new Apartment
        {
            AppNumber = model.AppNumber,
            Rooms = model.Rooms,
            Floor = model.Floor,
            IsFront = model.IsFront,
            PricePerNight = model.PricePerNight,
            IsReady = true, // новая квартира сразу готова к заселению
            UpdatedAt = DateTime.Now
        };

        _dbContext.Apartments.Add(apartment);
        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(ApartmentList));
    }

    [HttpGet]
    public async Task<IActionResult> ApartmentCreate()
    {
        return View();
    }

}


// bookings: view all, create booking, update booking, details booking ... etc.

// ? client + booking: