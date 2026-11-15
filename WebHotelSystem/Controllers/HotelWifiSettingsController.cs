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

    [HttpGet]
    public async Task<IActionResult> EditWifi(int id)
    {
        var settings = await  _dbContext.HotelWifiSettings.FindAsync(id);

        var form = new EditWifiViewModel() { Id = id ,WifiName = settings.WifiName, WifiPassword = settings.WifiPassword };
        return View(form);
    }

    [HttpPost]
    public async Task<IActionResult> EditWifi(EditWifiViewModel editWifi)
    {
        var settings = await  _dbContext.HotelWifiSettings.FindAsync(editWifi.Id);
        
        if (settings is null) return View();

        settings.WifiName = editWifi.WifiName ?? settings.WifiName; // старое = новое ЛИБО остается таким же, если ничего не меняется
        settings.WifiPassword = editWifi.WifiPassword ?? settings.WifiPassword;

         await _dbContext.SaveChangesAsync();

         return RedirectToAction(nameof(WifiSettings));




    }
}