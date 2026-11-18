using System;
using System.Collections.Generic;

namespace WebHotelSystem.Models;

public partial class GuestOrder
{
    public int Id { get; set; }

    public int AppNumb { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Description { get; set; } = null!;

    public int Guest { get; set; }

    public string OrderStatus { get; set; } = null!;

    public string? RealisedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Apartment AppNumbNavigation { get; set; } = null!;

    public virtual Client GuestNavigation { get; set; } = null!;
}
