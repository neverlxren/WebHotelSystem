namespace WebHotelSystem.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

public class HotelWifiSettingsViewModel
{
    public short? AppNumb { get; set; }
    public string? WifiName { get; set; } = string.Empty;
    public string? WifiPassword { get; set; } = string.Empty;
    public List<SelectListItem> ApartmentOptions { get; set; } = new();
}