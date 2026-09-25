using System.ComponentModel.DataAnnotations;

namespace WebHotelSystem.API.DTOs;

public class CreateOrderDto
{
    [Required] public string ClientName { get; set; } = null!;
    
    [Required] public short? AppNumb { get; set; } = null!;
    
   [Required] public string Description { get; set; } = null!;
    
    
    
}