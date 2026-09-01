using System.ComponentModel.DataAnnotations;

namespace OrderService.Models;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int RestaurantId { get; set; }

    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = "Placed";

    public List<OrderItem> Items { get; set; } = new();
}