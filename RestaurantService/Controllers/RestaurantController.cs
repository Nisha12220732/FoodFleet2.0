using Microsoft.AspNetCore.Mvc;
using RestaurantService.Data;
using RestaurantService.Models;

namespace RestaurantService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantController : ControllerBase
{
    private readonly RestaurantDbcontext _context;

    public RestaurantController(RestaurantDbcontext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AddRestaurant(Restaurant restaurant)
    {
        _context.Restaurants.Add(restaurant);
        _context.SaveChanges();

        return Ok(restaurant);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.Restaurants.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var restaurant = _context.Restaurants.Find(id);

        if (restaurant == null)
            return NotFound();

        return Ok(restaurant);
    }
}