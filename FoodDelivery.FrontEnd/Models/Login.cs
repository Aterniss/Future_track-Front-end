using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.FrontEnd.Models
{
    public class Login
    {
        [BindProperty]
        public string UserName { get; set; } = null!;
        [BindProperty]
        public string UserPassword { get; set; } = null!;
    }
}
