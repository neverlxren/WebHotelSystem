using WebHotelSystem.API.Data;
using WebHotelSystem.API.DTOs.Responses;
using WebHotelSystem.API.Models;

namespace WebHotelSystem.API.Services;


public class WebHotelService : IWebHotelService
{
    private readonly HotelDbContext _dbContext;

    public WebHotelService(HotelDbContext dbContext)
    {
        _dbContext = dbContext;
    } 
    
    public async Task<ApartmentResponseDto?> GetApartmentInfoById(int id)
    {
        var apart = await _dbContext.Apartments.FindAsync(id);
        
        // (statement) ? (true) : (false);
        return apart is null ? null : ToDto(apart);
        // if (apart is null)
        // {
        //     return null;
        // }
        // else
        // {
        //     return ToDto(apart);
        // }
    }
    
    
    //TODO: interfaces, converters to DTO, try to fill services

    private static ApartmentResponseDto ToDto(Apartment model) => new()
    {
        Id = model.Id,
        AppNumber = model.AppNumber,
        Floor = model.Floor,
        IsBalcon = model.IsBalcon,
        IsFront = model.IsFront,
        IsReady = model.IsReady,
        PricePerNight = model.PricePerNight,
        Rooms = model.Rooms,
        UpdatedAt = model.UpdatedAt

    };

    private static ClientResponseDto ToDto(Client model) => new()
    {
        ClientName = model.ClientName
    };
}