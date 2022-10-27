using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TFoodDelivery.FrontEnd.Models;

namespace FoodDelivery.FrontEnd.Pages.Admin.Restaurants
{
    public class MoreInfoModel : PageModel
    {
        private readonly IRestaurantServices _restaurant;
        public RestaurantModel OneRestaurant;
        public Account? Account { get; set; }

        public MoreInfoModel(IRestaurantServices restaurant)
        {
            this._restaurant = restaurant;
        }


        public async Task<IActionResult> OnGet(int id)
        {
            var check = HttpContext.Session.GetObject<Account>("Admin");
            if (check == null)
            {
                return Redirect("/Index");
            }
            var result = await _restaurant.GetById(id);
            OneRestaurant = result;
            return Page();
        }
    }
}
