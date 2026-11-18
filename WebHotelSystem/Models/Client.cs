using System;
using System.Collections.Generic;

namespace WebHotelSystem.Models;

public partial class Client
{
    public int Id { get; set; }

    public string ClientName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string Email { get; set; } = null!;

    public short GuestCount { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string RegStatus { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<GuestOrder> GuestOrders { get; set; } = new List<GuestOrder>();
}
