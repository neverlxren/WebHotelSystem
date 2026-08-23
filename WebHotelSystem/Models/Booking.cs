using System;
using System.Collections.Generic;

namespace WebHotelSystem.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int? AppBookId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Description { get; set; }

    public virtual Apartment? AppBook { get; set; }
}
