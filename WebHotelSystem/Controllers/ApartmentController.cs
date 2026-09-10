using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebHotelSystem.Data;
using WebHotelSystem.Models;

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
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> ApartmentList()
    {
        var apartments = await _dbContext.Apartments
            .ToListAsync();

        return View(apartments);
    }
    
}

// [HttpGet]
    // public async Task<IActionResult> Details(int id)
    // {
    //     var apartment = await _dbContext.Apartments
    //         .FirstOrDefaultAsync(a => a.Id == id);
    //
    //     if (apartment == null)
    //     {
    //         return NotFound();
    //     }
    //
    //     return View(apartment);
    // }
    
   
    

// bookings: view all, create booking, update booking, details booking ... etc.

// ? client + booking: