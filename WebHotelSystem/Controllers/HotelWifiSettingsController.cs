using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebHotelSystem.Data;
using WebHotelSystem.Models;
using WebHotelSystem.ViewModels;

namespace WebHotelSystem.Controllers;

public class HotelWifiSettingsController : Controller
{
    private readonly HotelDbContext _dbContext;

    public HotelWifiSettingsController(HotelDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Страница со списком сетей
    [HttpGet]
    public async Task<IActionResult> WifiSettings()
    {
        var settings = await _dbContext.HotelWifiSettings
            .OrderBy(setting => setting.AppNumb)
            .ToListAsync();

        return View("HotelWifiSettings", settings);
    }

    // добавления сети
    [HttpGet]
    public async Task<IActionResult> AddWifi()
    {
        var model = new HotelWifiSettingsViewModel();
        await FillApartmentOptions(model);

        return View(model); 
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddWifi(HotelWifiSettingsViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await FillApartmentOptions(model);
            return View(model);
        }

        var setting = new HotelWifiSetting
        {
            AppNumb = model.AppNumb,
            WifiName = model.WifiName ?? string.Empty,
            WifiPassword = model.WifiPassword ?? string.Empty,
            UpdatedAt = DateTime.Now
        };

        _dbContext.HotelWifiSettings.Add(setting);
        await _dbContext.SaveChangesAsync();

        return RedirectToAction(nameof(WifiSettings));
    }

    private async Task FillApartmentOptions(HotelWifiSettingsViewModel model)
    {
        model.ApartmentOptions = await _dbContext.Apartments
            .OrderBy(apartment => apartment.AppNumber)
            .Select(apartment => new SelectListItem
            {
                Value = apartment.AppNumber.ToString(),
                Text = apartment.AppNumber.ToString()
            })
            .ToListAsync();
    }
}