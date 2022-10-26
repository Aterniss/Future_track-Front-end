using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.FrontEnd.Models.Requests
{
    public class DishRequest
    {
        [BindProperty]
        public string? DishName { get; set; }
        [BindProperty]
        public string DishDescription { get; set; } = null!;
        [BindProperty]
        public int RestaurantId { get; set; }
        [BindProperty]
        public decimal Price { get; set; }
        [BindProperty]
        public bool? Require18 { get; set; }
    }
}
