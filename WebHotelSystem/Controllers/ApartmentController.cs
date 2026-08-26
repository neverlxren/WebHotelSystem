using Microsoft.AspNetCore.Mvc;
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
    public IActionResult Index()
    {
        return View(_dbContext.Apartments);
    }
}



//TODO: create clientDB -> rescaffold -> upload; all 3 DB's functions ++ check scaffold file in obdisidian 

// bookings: view all, create booking, update booking, details booking ... etc.

// ? client + booking: