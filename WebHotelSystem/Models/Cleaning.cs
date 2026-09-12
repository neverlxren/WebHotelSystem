using System;
using System.Collections.Generic;

namespace WebHotelSystem.Models;

public partial class Cleaning
{
    public int Id { get; set; }

    public short AppNumb { get; set; }

    public string CleaningStatus { get; set; } = null!;

    public string RealisedBy { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Apartment AppNumbNavigation { get; set; } = null!;
}
