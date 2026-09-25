namespace WebHotelSystem.API.DTOs.Responses;

public class ClientResponseDto
{
    public int Id { get; set; }

    public string ClientName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string RegStatus { get; set; } = null!;

    public short? GuestCount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}