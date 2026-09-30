using Microsoft.AspNetCore.Mvc;
using WebHotelSystem.API.DTOs;
using WebHotelSystem.API.DTOs.Responses;
using WebHotelSystem.API.Services;

namespace WebHotelSystem.API.Controllers;

[ApiController]
[Route("api/")]
public class ApartmentController : ControllerBase
{
    private readonly IWebHotelService _webHotelService;

    public ApartmentController(IWebHotelService webHotelService)
    {
        _webHotelService = webHotelService;
    }

    [HttpGet("apartment/info={id:int}")]
    public async Task<IActionResult> GetAppInfoAsync(int id)
    {
        var apart = await _webHotelService.GetApartmentInfoById(id);

        if (apart is null) return NotFound(new { message = "error, cant find this app" });
        return Ok(apart);
    }

    [HttpPatch("{id}")] //переспросить 
    public async Task<ActionResult<ApartmentResponseDto>> EditApartmentAsync(int id, ApartmentEditDto dto)
    {
        var apartment = await _webHotelService.EditApartment(dto, id);

        if (apartment is null)
            return NotFound();

        return Ok(apartment);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> BlockApartmentAsync(int id)
    {
        var apartment = await _webHotelService.BlockApartment(id);

        if (apartment is false) //там булевое значение 
            return NotFound();
        return NoContent();
    }
    
    //TODO: controllers for exist methods .... new services

}