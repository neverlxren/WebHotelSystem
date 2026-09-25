namespace WebHotelSystem.API.DTOs;

public class EditClientDto
{
  
    public string? ClientName { get; set; } 
    
    public string? PhoneNumber { get; set; } 
    
    public string? Email { get; set; }
    
    public short? GuestCount { get; set; }
}