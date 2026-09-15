using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebHotelSystem.Data;
using WebHotelSystem.Models;
using WebHotelSystem.ViewModels;

namespace WebHotelSystem.Controllers;

public class GuestOrderController : Controller
{
    private readonly HotelDbContext _dbContext;
 
    public GuestOrderController(HotelDbContext dbContext)
    {
        _dbContext = dbContext;
    }
 
    [HttpGet]
    public async Task<IActionResult> GuestOrders()
    {
        var orders = await _dbContext.GuestOrders
            .Include(order => order.GuestNavigation)
            .Include(order => order.AppNumbNavigation)
            .OrderByDescending(order => order.CreatedAt)
            .ToListAsync();
 
        return View(orders);
    }
 
    [HttpGet]
    public IActionResult Create()
    {
        return View("NewOrder", new NewGuestOrderViewModel());
    }
 
    [HttpPost]
    public async Task<IActionResult> Create(NewGuestOrderViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("NewOrder", model);
        }
 
        // Проверяем, что квартира с таким номером существует
        var apartmentExists = await _dbContext.Apartments
            .AnyAsync(a => a.AppNumber == model.AppNumb);
 
        if (!apartmentExists)
        {
            ModelState.AddModelError(nameof(model.AppNumb), "Квартира с таким номером не найдена");
            return View("NewOrder", model);
        }
        // Ищем существующего гостя по имени
        var client = await _dbContext.Clients
            .FirstOrDefaultAsync(c => c.ClientName == model.ClientName);
 
        // Если гостя нет — создаём нового
        if (client is null)
        {
            client = new Client
            {
                ClientName = model.ClientName,
                PhoneNumber = "", // TODO: заглушка, т.к. формы заявки не спрашивает телефон
                Email = "",       // TODO: заглушка, т.к. форма заявки не спрашивает email
                RegStatus = "registred",
                GuestCount = 1,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
 
            _dbContext.Clients.Add(client);
            await _dbContext.SaveChangesAsync(); // сохраняем, чтобы получить client.Id
        }
 
        var order = new GuestOrder
        {
            AppNumb = model.AppNumb,
            Guest = client.Id,
            OrderStatus = "new",
            Description = model.Description,
            RealisedBy = "", // исполнитель ещё не назначен; колонка в БД NOT NULL
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
 
        _dbContext.GuestOrders.Add(order);
        await _dbContext.SaveChangesAsync();
 
        return RedirectToAction(nameof(GuestOrders));
    }
}
 