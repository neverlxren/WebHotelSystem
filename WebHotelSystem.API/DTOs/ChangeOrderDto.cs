using System.ComponentModel.DataAnnotations;

namespace WebHotelSystem.API.DTOs;

public class ChangeOrderDto
{ [Required]
        public string Status { get; set;} = null!;
}