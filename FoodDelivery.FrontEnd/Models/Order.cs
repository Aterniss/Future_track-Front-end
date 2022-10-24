using TFoodDelivery.FrontEnd.Models;

namespace FoodDelivery.FrontEnd.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string? OrderStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public int RiderId { get; set; }
        public int IdUser { get; set; }

        public virtual User IdUserNavigation { get; set; } = null!;
        public virtual Rider Rider { get; set; } = null!;
        public virtual ICollection<OrderDish>? OrderDishes { get; set; }
    }
}
