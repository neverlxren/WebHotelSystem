using WebHotelSystem.API.DTOs;
using WebHotelSystem.API.DTOs.Responses;

namespace WebHotelSystem.API.Services;

public interface IWebHotelService
{
    //  ------------  Apartament 
    Task<IEnumerable<ApartmentResponseDto>> GetAllApartments();

    Task<ApartmentResponseDto?> GetApartmentInfoById(int id);

    Task<ApartmentResponseDto?> EditApartment(ApartmentEditDto dto, int id);

    Task<bool> BlockApartment(int id);

    // -------------  Client

    Task<IEnumerable<ClientResponseDto>> GetAllClients();

    Task<ClientResponseDto?> EditClient(EditClientDto dto, int id);

    Task<ClientResponseDto?> GetClientInfoById(int id);

    Task<bool> DeleteClient(int id);

    Task<ClientResponseDto?> CreateClient(CreateClientDto dto);
    
    

// -------------  Order
    Task<IEnumerable<OrderResponseDto>> GetAllOrders();

    Task<OrderResponseDto?> CreateOrder(CreateOrderDto dto); 

    Task<OrderResponseDto?> GetOrderInfoById(int id);

    Task<OrderResponseDto?> ChangeOrderStatus(int id);
}
    