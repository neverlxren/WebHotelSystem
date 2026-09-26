using Microsoft.EntityFrameworkCore; // нужен для ToListAsync()
using WebHotelSystem.API.Data;
using WebHotelSystem.API.DTOs; // нужен для EditClientDto / CreateClientDto / ApartmentEditDto
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


    // --------Converters
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
        Id = model.Id,
        ClientName = model.ClientName,
        PhoneNumber = model.PhoneNumber,
        CreatedAt = model.CreatedAt,
        Email = model.Email,
        GuestCount = model.GuestCount,
        RegStatus = model.RegStatus,
        UpdatedAt = model.UpdatedAt

    };
    
    private static OrderResponseDto ToDto(GuestOrder model) => new()
    {
        Id = model.Id,
        AppNumb = model.AppNumb,
        CreatedAt = model.CreatedAt,
        Description = model.Description,
        Guest = model.Guest,
        OrderStatus = model.OrderStatus,
        RealisedBy = model.RealisedBy,
        UpdatedAt = model.UpdatedAt
    }
}

