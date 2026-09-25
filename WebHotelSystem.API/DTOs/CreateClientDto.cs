using System.ComponentModel.DataAnnotations;

namespace WebHotelSystem.API.DTOs;

public class CreateClientDto
{
    [Required]
    [MinLength(2)]
    [MaxLength(64)]
    public string ClientName { get; set; } = null!; 
    
    [Required]
    [Phone]
    [MaxLength(12)]
    public string PhoneNumber { get; set; } = null!;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    
    [Range(1, 6)]
    public short? GuestCount { get; set; }

}