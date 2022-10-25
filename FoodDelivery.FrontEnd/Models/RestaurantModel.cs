using FoodDelivery.FrontEnd.Models;

namespace TFoodDelivery.FrontEnd.Models
{
    public class RestaurantModel
    {
        public int RestaurantId { get; set; }
        public string? RestaurantName { get; set; }
        public string? CategoryName { get; set; }
        public string? RestaurantAddress { get; set; }
        public int ZoneId { get; set; }

        public virtual FoodCategory? CategoryNameNavigation { get; set; }
        public virtual Zone Zone { get; set; } = null!;
        public virtual ICollection<Dish>? Dishes { get; set; }
    }
}
