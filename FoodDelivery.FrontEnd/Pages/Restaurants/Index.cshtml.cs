using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TFoodDelivery.FrontEnd.Models;

namespace FoodDelivery.FrontEnd.Pages.Restaurants
{
    public class IndexModel : PageModel
    {
        private readonly IRestaurantServices _restaurant;

        public IEnumerable<RestaurantModel>? Restaurants;
        public RestaurantModel? Restaurant;

        public IndexModel(IRestaurantServices restaurant)
        {
            _restaurant = restaurant;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var result = await _restaurant.GetAll();
            Restaurants = result;
            return Page();
        }

       
    }
}
