using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.FrontEnd.Models.Requests
{
    public class RiderRequest
    {
        [BindProperty]
        public string RiderName { get; set; } = null!;
        [BindProperty]
        public int ZoneId { get; set; }
    }
}
