using System.ComponentModel.DataAnnotations;

namespace WebHotelSystem.ViewModels;

public class RegisterViewModel
{
    [Required]
    [MinLength(2, ErrorMessage = "input min 2 symbols")]
    [MaxLength(64)]
    public string ClientName { get; set; } = null!; 
    
    [Required]
    [Phone(ErrorMessage = "phone number should be no longer then 12 symbols(+ isnt included)")]
    [MaxLength(12)]
    public string PhoneNumber { get; set; } = null!;
    
    [Required]
    [EmailAddress(ErrorMessage = "email must contain '@' symbol ")]
    public string Email { get; set; } = null!;
    
    [Range(1, 6)]
    public short? GuestCount { get; set; }
    
}