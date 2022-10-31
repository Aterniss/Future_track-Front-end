using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.FrontEnd.Models.Requests
{
    public class RestaurantRequest
    {
        [BindProperty]
        public string? RestaurantName { get; set; }
        [BindProperty]
        public string CategoryName { get; set; }
        [BindProperty]
        public string RestaurantAddress { get; set; }
        [BindProperty]
        public int ZoneId { get; set; }
    }
}
