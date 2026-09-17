using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebHotelSystem.Data;

namespace WebHotelSystem.Controllers;

public class HotelWifiSettingsController : Controller
{
    private readonly HotelDbContext _dbContext;

    public HotelWifiSettingsController(HotelDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET
    [HttpGet]
    public async Task<IActionResult> HotelWifiSettings()
    {
        var networks = await _dbContext.HotelWifiSettings
            .OrderBy(w => w.Id)
            .ToListAsync();

        return View("HotelWifiSettings", networks);
    }
}