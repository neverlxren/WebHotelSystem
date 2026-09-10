using System;
using System.Collections.Generic;

namespace WebHotelSystem.Models;

public partial class Apartment
{
    public int Id { get; set; }

    public short AppNumber { get; set; }

    public short Rooms { get; set; }
    
   // public short Capacity { get; set; } //max amount of guests

    public bool? IsBalcon { get; set; }

    public bool? IsFront { get; set; }

    public short Floor { get; set; }

    public bool IsReady { get; set; }

    public decimal PricePerNight { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
