using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;
using Microsoft.OpenApi;
using WebHotelSystem.API.Data;
using WebHotelSystem.API.DTOs; 
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

    // --------------ApartmentService
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

    public async Task<ApartmentResponseDto?> EditApartment(ApartmentEditDto dto, int id)
    {
        var apart = await _dbContext.Apartments.FindAsync(id);

        if (apart is null) return null;
        
        apart.AppNumber = dto.AppNumber ?? apart.AppNumber;
        apart.PricePerNight = dto.Price ?? apart.PricePerNight;
        apart.Rooms = dto.Rooms ?? apart.Rooms;

        await _dbContext.SaveChangesAsync();
        
        return ToDto(apart);
    }

    public Task<IEnumerable<ApartmentResponseDto>> GetAllApartments()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> BlockApartment(int id)
    {
        var apart = await _dbContext.Apartments.FindAsync(id);

        if (apart is null) return false;

        apart.IsReady = false;

        await _dbContext.SaveChangesAsync();

        return true;
    }
    
    
    //   --------------ClientService


    public async Task<ClientResponseDto?> GetClientInfoById(int id)
    {
        var client = await _dbContext.Clients.FindAsync(id);

        return client is null ? null : ToDto(client);
    }

    //TODO: fix EditCLient
    public async Task<ClientResponseDto?> EditClient(EditClientDto dto, int id)
    {
        var client = await _dbContext.Clients.FindAsync(id);

        client.ClientName = dto.ClientName;
        client.PhoneNumber = dto.PhoneNumber;
        client.Email = dto.Email;
        client.GuestCount = dto.GuestCount;

        return client is null ? null : ToDto(client);
    }

    public Task<IEnumerable<ClientResponseDto>> GetAllClients()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteClient(int id)
    {
        var client = await _dbContext.Clients.FindAsync(id);
        
        if (client is null) return false;

        _dbContext.Clients.Remove(client); 
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<ClientResponseDto?> CreateClient (CreateClientDto dto)
    {
        var isActiveEmail = _dbContext.Clients.Any(x => x.Email == dto.Email);
        var isActivePhoneNumber = _dbContext.Clients.Any(y => y.PhoneNumber == dto.PhoneNumber);

        if (isActiveEmail || isActivePhoneNumber) return null;
        
        var client = new Client
        {
            ClientName = dto.ClientName,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            GuestCount = dto.GuestCount

        };
        _dbContext.Clients.Add(client);

        await _dbContext.SaveChangesAsync();
        return ToDto(client);
    }
    
//   --------------OrderService

public async Task<OrderResponseDto?> GetOrderInfoById(int id)
{
    var order  = await _dbContext.GuestOrders.FindAsync(id);

    return order is null ? null : ToDto(order);
}

// public Task<OrderResponseDto?> CreateOrder(CreateOrderDto dto)
// {
//     // var order = new GuestOrder
//     // {
//     //     Guest = _dbContext.Clients.,
//     //     Description = dto.Description,
//     //     AppNumb = dto.AppNumb
//     // };
// }
public Task<OrderResponseDto?> CreateOrder(CreateOrderDto dto)
{
    throw new NotImplementedException();
}

public Task<OrderResponseDto?> ChangeOrderStatus(int id)
{
    throw new NotImplementedException();
}

public Task<IEnumerable<OrderResponseDto>> GetAllOrders()
{
    throw new NotImplementedException();
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
    };
}

