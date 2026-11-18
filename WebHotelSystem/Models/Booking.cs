using System;
using System.Collections.Generic;

namespace WebHotelSystem.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int AppNumb { get; set; }

    public int? GuestId { get; set; }

    public short AmountNights { get; set; }

    public DateTime BookingStart { get; set; }

    public DateTime BookingEnd { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Apartment AppNumbNavigation { get; set; } = null!;

    public virtual Client? Guest { get; set; }
}
