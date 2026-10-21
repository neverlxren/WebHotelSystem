using System.Runtime.InteropServices.JavaScript;
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

    public async Task<IEnumerable<ApartmentResponseDto>> GetAllApartments()
    {
        var apartList =  await _dbContext.Apartments
            .Select(x => ToDto(x))
            .ToListAsync();
        return(apartList);
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
    
    public async Task<ClientResponseDto?> EditClient(EditClientDto dto, int id)
    {
        var client = await _dbContext.Clients.FindAsync(id);

        if (client is null) return null;

        client.ClientName = dto.ClientName ?? client.ClientName;
        client.PhoneNumber = dto.PhoneNumber ?? client.PhoneNumber;
        client.Email = dto.Email ?? client.Email;
        client.GuestCount = dto.GuestCount ?? client.GuestCount;

        await _dbContext.SaveChangesAsync();

        return ToDto(client);
    }

    
    public async Task<IEnumerable<ClientResponseDto>> GetAllClients()
    {
        var clientList = await _dbContext.Clients.Select(x => ToDto(x)).
            ToListAsync();

        return (clientList);
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

public async Task<OrderResponseDto?> CreateOrder(CreateOrderDto dto)
{
    // var guest = await _dbContext.Clients.SingleAsync(x => x.ClientName == dto.ClientName);
    var order = new GuestOrder()
    {
        AppNumb = dto.AppNumb,
        Guest = dto.ClientId,
        OrderStatus = "new",
        Description = dto.Description,
        CreatedAt = DateTime.Now,
        RealisedBy = "",
        
        //todo: remake OrderDto
    };
    _dbContext.GuestOrders.Add(order);

    await _dbContext.SaveChangesAsync();

    return ToDto(order);
}

public async Task<OrderResponseDto?> ChangeOrderStatus(int id, string status)
{
    var order = await _dbContext.GuestOrders.FindAsync(id);

    if (order is null)
        return null;

    order.OrderStatus = status;
    await _dbContext.SaveChangesAsync();

    return ToDto(order);
}

public async Task<IEnumerable<OrderResponseDto>> GetAllOrders()
{
    var orderlist = await _dbContext.GuestOrders.Select(x =>  ToDto(x)).ToListAsync();
    return orderlist;
}




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

