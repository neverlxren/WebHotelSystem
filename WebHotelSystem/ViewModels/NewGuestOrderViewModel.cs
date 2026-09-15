using System.ComponentModel.DataAnnotations;

namespace WebHotelSystem.ViewModels;

public class NewGuestOrderViewModel
{
   [Required]
   [MinLength(2, ErrorMessage = "input full name")]
   [MaxLength(128)]
   public string ClientName { get; set; } = null!;

   [Required] public short? AppNumb { get; set; } = null!;

   [Required]
   [MinLength(2, ErrorMessage = "write more description of problem")]
   [MaxLength(600)]
   public string Description { get; set; } = null!;

}