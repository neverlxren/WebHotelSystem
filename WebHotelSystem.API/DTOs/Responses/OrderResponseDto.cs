namespace WebHotelSystem.API.DTOs.Responses;

public class OrderResponseDto
{
    public int Id { get; set; }

    public short? AppNumb { get; set; }

    public int? Guest { get; set; }

    public string OrderStatus { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string RealisedBy { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}