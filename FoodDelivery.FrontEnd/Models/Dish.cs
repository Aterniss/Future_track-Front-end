namespace FoodDelivery.FrontEnd.Models
{
    public class Dish
    {
        public int DishId { get; set; }
        public string? DishName { get; set; }
        public string DishDescription { get; set; } = null!;
        public int RestaurantId { get; set; }
        public decimal Price { get; set; }
        public bool? Require18 { get; set; }
    }
}
