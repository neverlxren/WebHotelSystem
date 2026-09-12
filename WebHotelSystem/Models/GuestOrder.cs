using System;
using System.Collections.Generic;

namespace WebHotelSystem.Models;

public partial class GuestOrder
{
    public int Id { get; set; }

    public short? AppNumb { get; set; }

    public int? Guest { get; set; }

    public string OrderStatus { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string RealisedBy { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Apartment? AppNumbNavigation { get; set; }

    public virtual Client? GuestNavigation { get; set; }
}
