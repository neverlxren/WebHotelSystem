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

    [HttpGet("view/apartment={id:int}")]
    public async Task<IActionResult> GetAppInfoAsync(int id)
    {
        var apart = await _webHotelService.GetApartmentInfoById(id);

        if (apart is null) return NotFound(new { message = "error, cant find this app" });
        return Ok(apart);
    }

    [HttpPatch("edit/apartment={id:int}")] //переспросить 
    public async Task<IActionResult> EditApartmentAsync([FromBody] ApartmentEditDto dto, int id)
    {
        var apartment = await _webHotelService.EditApartment(dto, id);

        if (apartment is null) return NotFound();

        return Ok(apartment);
    }

    [HttpDelete("delete/apartment={id:int}")]
    public async Task<IActionResult> BlockApartmentAsync(int id)
    {
        var apartment = await _webHotelService.BlockApartment(id);

        if (apartment is false) //там булевое значение 
            return NotFound();
        return NoContent();
    }


    [HttpGet("view/apartments")]
    public async Task<IActionResult> GetAllApartments()
    {
        var apartments = await _webHotelService.GetAllApartments();

        return Ok(apartments);
    }
    //TODO: controllers for exist methods .... new services


    
    
    // api/{model}/{action}/{params}
    
    // api/aparts/delete
    // api/aparts/edit/{id}
    
    // ---
    
    
    // api/{action}/{model}/{params}
    
    // api/view/appps
    // api/view/client={id}
    
}