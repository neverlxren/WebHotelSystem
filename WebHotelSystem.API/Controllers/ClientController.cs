using Microsoft.AspNetCore.Mvc;
using WebHotelSystem.API.DTOs;
using WebHotelSystem.API.DTOs.Responses;
using WebHotelSystem.API.Services;

namespace WebHotelSystem.API.Controllers;
[ApiController]
[Route("api/")]
public class ClientController : ControllerBase
{  
    private readonly IWebHotelService _webHotelService;

    public ClientController(IWebHotelService webHotelService)
    {
        _webHotelService = webHotelService;
    }

    [HttpPost("client/create")]
    public async Task<IActionResult> CreateClientAsync([FromBody] CreateClientDto dto)
    {
        var client = await _webHotelService.CreateClient(dto);

        if (client is null) return Conflict(new{message = "error, something goes wrong with data"});
        return Ok(client);
    }

    [HttpGet("client/allclients")]
    public async Task<IActionResult> GetClientInfoAsync(int id)
    {
        var client = await _webHotelService.GetApartmentInfoById(id);

        if (client is null)
            return NotFound(new {message = "error, can't find this apartment"});

        return Ok();
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<ClientResponseDto>> EditClientAsync(int id, EditClientDto dto)
    {
        var client = await _webHotelService.EditClient(dto, id);

        if (client is null) return NotFound();

        return Ok(client);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClientAsync(int id)
    {
        var deleted = await _webHotelService.DeleteClient(id); //client -> deleted cuz result of operation
        
        if (deleted is false) return NotFound();
        return Ok(deleted);
    }

   
 }