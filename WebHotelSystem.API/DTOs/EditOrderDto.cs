namespace WebHotelSystem.API.DTOs;

public class EditOrderDto
{
    public int? ClientId { get; set; }
    public string? Description { get; set; }
    public short? AppNumb { get; set; }
    // public short? GuestCount { get; set; }
}