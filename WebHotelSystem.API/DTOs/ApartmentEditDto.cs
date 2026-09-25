namespace WebHotelSystem.API.DTOs;

public class ApartmentEditDto
{
    public string ApartmentNumber { get; set; } = null!;
    
    public decimal Price { get; set; }
    
    public short Capacity { get; set; }
}