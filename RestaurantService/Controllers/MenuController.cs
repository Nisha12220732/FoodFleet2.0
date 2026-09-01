using Microsoft.AspNetCore.Mvc;
using RestaurantService.Data;
using RestaurantService.Models;

namespace RestaurantService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly RestaurantDbcontext _context;

    public MenuController(RestaurantDbcontext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AddMenuItem(MenuItem menuItem)
    {
        _context.MenuItems.Add(menuItem);
        _context.SaveChanges();

        return Ok(menuItem);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.MenuItems.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var menuItem = _context.MenuItems.Find(id);

        if (menuItem == null)
            return NotFound();

        return Ok(menuItem);
    }
}