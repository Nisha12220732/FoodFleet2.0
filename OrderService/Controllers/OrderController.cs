using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using OrderService.Data;
using OrderService.Models;

namespace OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderDbContext _context;
    private readonly HttpClient _httpClient;

    public OrderController(
        OrderDbContext context,
        HttpClient httpClient)
    {
        _context = context;
        _httpClient = httpClient;
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(Order order)
    {
        decimal total = 0;

        foreach (var item in order.Items)
        {
            var menuItem = await _httpClient.GetFromJsonAsync<MenuItem>(
                $"http://localhost:5052/api/Menu/{item.MenuItemId}");

            if (menuItem == null)
                return NotFound();

            item.Price = menuItem.Price;
            total += item.Price * item.Quantity;
        }

        order.TotalAmount = total;

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return Ok(order);
    }

    [HttpGet]
    public IActionResult GetAllOrders()
    {
        return Ok(_context.Orders.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetOrderById(int id)
    {
        var order = _context.Orders.Find(id);

        if (order == null)
            return NotFound();

        return Ok(order);
    }

}