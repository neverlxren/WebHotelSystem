using System.ComponentModel.DataAnnotations;

namespace WebHotelSystem.ViewModels;

public class RegisterViewModel
{
    [Required]
    [MinLength(2, ErrorMessage = "input min 2 symbols")]
    [MaxLength(64)]
    public string ClientName { get; set; } = null!; 
    
    [Required]
    [Phone(ErrorMessage = "")]
    public string PhoneNumber { get; set; } = null!;
    
    [Required]
    [EmailAddress(ErrorMessage = "")]
    public string Email { get; set; } = null!;
    
    public short? GuestCount { get; set; }
    
}