namespace FoodDelivery.FrontEnd.Models
{
    public class OrderDish
    {
        public int OrderId { get; set; }
        public int DishId { get; set; }

        public virtual Dish Dish { get; set; } = null!;
    }
}
