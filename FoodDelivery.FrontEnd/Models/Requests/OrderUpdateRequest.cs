using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.FrontEnd.Models.Requests
{
    public class OrderUpdateRequest
    {
        [BindProperty]
        public string OrderStatus { get; set; } = null!;
        [BindProperty]
        public int RiderId { get; set; }
        [BindProperty]
        public int IdUser { get; set; }
    }
}
