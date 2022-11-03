using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TFoodDelivery.FrontEnd.Models;

namespace FoodDelivery.FrontEnd.Pages.Restaurants
{
    public class ViewModel : PageModel
    {
        private readonly IRestaurantServices _restaurant;

        public RestaurantModel Restaurant;
        public string Message { get; set; }


        public ViewModel(IRestaurantServices restaurant)
        {
            _restaurant = restaurant;
            
           
        }

        public async Task<IActionResult> OnGet(int id)
        {
            try
            {
                var result = await _restaurant.GetById(id);
                Restaurant = result;
                return Page();
            }
            catch(HttpRequestException e)
            {
                Message = e.Message;
                return Page();
            }
        }

    }
}
