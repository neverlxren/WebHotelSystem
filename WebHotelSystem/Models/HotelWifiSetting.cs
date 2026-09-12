using System;
using System.Collections.Generic;

namespace WebHotelSystem.Models;

public partial class HotelWifiSetting
{
    public int Id { get; set; }

    public short? AppNumb { get; set; }

    public string WifiName { get; set; } = null!;

    public string WifiPassword { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public virtual Apartment? AppNumbNavigation { get; set; }
}
