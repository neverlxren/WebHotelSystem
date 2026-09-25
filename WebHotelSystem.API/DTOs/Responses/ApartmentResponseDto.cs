namespace WebHotelSystem.API.DTOs.Responses;

public class ApartmentResponseDto
{
    public int Id { get; set; }

    public short AppNumber { get; set; }

    public short Rooms { get; set; }

    public bool? IsBalcon { get; set; }

    public bool? IsFront { get; set; }

    public short Floor { get; set; }

    public bool IsReady { get; set; }

    public decimal PricePerNight { get; set; }

    public DateTime? UpdatedAt { get; set; }
}