using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.FrontEnd.Models.Requests
{
    public class AccountRequest
    {
        [BindProperty]
        public string UserName { get; set; } = null!;
        [BindProperty]
        public string UserPassword { get; set; } = null!;
        [BindProperty]
        public string FullName { get; set; } = null!;
        [BindProperty]
        public string UserAddress { get; set; } = null!;
        [BindProperty]
        public bool? IsOver18 { get; set; }
        [BindProperty]
        public string EmailAddress { get; set; } = null!;
        [BindProperty]
        public string? TelNumber { get; set; }
        [BindProperty]
        public int? IdUsers { get; set; }
        [BindProperty]
        public int? RestaurantId { get; set; }
        [BindProperty]
        public int Role { get; set; }
    }
}
