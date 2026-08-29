using System.ComponentModel.DataAnnotations;

namespace WebHotelSystem.ViewModels;

public class ApartmentViewModel
{
    [Required(ErrorMessage = "input apartment number")] 
    [MaxLength(3, ErrorMessage = "app number should contain 3 symbols")]
    public string ApartmentNumber { get; set; } = null!;
    
    public decimal Price { get; set; }
    
    [Range(1,6, ErrorMessage = "Capacity should be no more then 6 guests")]
    public short Capacity { get; set; }
}