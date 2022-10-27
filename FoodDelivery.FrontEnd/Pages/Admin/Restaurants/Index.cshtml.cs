using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TFoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using FoodDelivery.FrontEnd.Models;

namespace FoodDelivery.FrontEnd.Pages.Admin.Restaurants
{
    public class IndexModel : PageModel
    {
        private readonly IRestaurantServices _restaurant;
        public IEnumerable<RestaurantModel>? Restaurants { get; set; }
        public RestaurantModel? OneRestaurant { get; set; }
        public Account? Account { get; set; }


        public IndexModel(IRestaurantServices restaurant)
        {
            this._restaurant = restaurant;
        }


        public async Task<IActionResult> OnGetAsync()
        {

            var check = HttpContext.Session.GetObject<Account>("Admin");
            if (check == null)
            {
                return Redirect("/Index");
            }
            var result = await _restaurant.GetAll();
            Restaurants = result;
            Account = check;
            return Page();

        }
    }
}
