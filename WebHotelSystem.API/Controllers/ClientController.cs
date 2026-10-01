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

    [HttpPost("create/client")]
    public async Task<IActionResult> CreateClientAsync([FromBody] CreateClientDto dto)
    {
        var client = await _webHotelService.CreateClient(dto);

        if (client is null) return Conflict(new{message = "error, something goes wrong with data"});
        return Ok(client);
    }

    [HttpGet("view/client={id:int}")]
    public async Task<IActionResult> GetClientInfoAsync(int id)
    {
        var client = await _webHotelService.GetApartmentInfoById(id);

        if (client is null)
            return NotFound(new {message = "error, can't find this apartment"});

        return Ok();
    }

    [HttpPatch("edit/client={id:int}")]
    public async Task<IActionResult> EditClientAsync([FromBody] EditClientDto dto, int id)
    {
        var client = await _webHotelService.EditClient(dto, id);

        if (client is null) return NotFound();

        return Ok(client);
    }

    [HttpDelete("delete/client={id:int}")]
    public async Task<IActionResult> DeleteClientAsync(int id)
    {
        var deleted = await _webHotelService.DeleteClient(id); //client -> deleted cuz result of operation
        
        if (deleted is false) return NotFound();
        return Ok(deleted);
    }

    [HttpGet("view/clients")]
    public async Task<IActionResult> GetAllClients()
    {
        var clients = await _webHotelService.GetAllClients();

        return Ok(clients);
    }
   
 }