using TFoodDelivery.FrontEnd.Models;

namespace FoodDelivery.FrontEnd.Models
{
    public class Account
    {
        public int Id { get; set; }

        public string UserName { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string? TelNumber { get; set; }
        public int? IdUsers { get; set; }
        public int? RestaurantId { get; set; }
        public int Role { get; set; }

        public virtual User? IdUsersNavigation { get; set; }
        public virtual RestaurantModel? Restaurant { get; set; }
        public virtual Role RoleNavigation { get; set; } = null!;
    }
}
