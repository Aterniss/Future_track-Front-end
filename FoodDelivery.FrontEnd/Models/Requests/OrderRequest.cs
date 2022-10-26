using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.FrontEnd.Models.Requests
{
    public class OrderRequest
    {
        [BindProperty]
        public string? OrderStatus { get; set; }
        [BindProperty]
        public int RiderId { get; set; }
        [BindProperty]
        public int IdUser { get; set; }
        [BindProperty]
        public List<int> Dishes { get; set; }

    }
}
