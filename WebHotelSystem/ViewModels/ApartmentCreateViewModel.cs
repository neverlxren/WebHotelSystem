using System.ComponentModel.DataAnnotations;

namespace WebHotelSystem.ViewModels;

public class ApartmentCreateViewModel
{
    [Required]
    [Range(100, 999, ErrorMessage = "Apartment number must be 3 digits")]
    public short AppNumber { get; set; }

    [Required]
    [Range(1, 3, ErrorMessage = "Rooms: 1 to 3")]
    public short Rooms { get; set; }

    [Required]
    [Range(0, 6, ErrorMessage = "Floor: 0 to 6")]
    public short Floor { get; set; }
    
    public bool IsFront { get; set; }

    [Required]
    [Range(0.01, 100000)]
    public decimal PricePerNight { get; set; }
}