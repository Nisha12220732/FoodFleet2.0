using System.ComponentModel.DataAnnotations;

namespace OrderService.Models;

public class OrderItem
{
    [Key]
    public int OrderItemId { get; set; }

    public int OrderId { get; set; }

    [Required]
    public int MenuItemId { get; set; }

    [Required]
    public int Quantity { get; set; }

    public decimal Price { get; set; }
}