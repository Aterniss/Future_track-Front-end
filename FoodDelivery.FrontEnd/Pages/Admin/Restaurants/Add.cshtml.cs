using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Models.Requests;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TFoodDelivery.FrontEnd.Models;

namespace FoodDelivery.FrontEnd.Pages.Admin.Restaurants
{
    public class AddModel : PageModel
    {
        private readonly IRestaurantServices _restaurant;
        public Account? Account { get; set; }
        public RestaurantRequest? Request { get; set; }
        public string Message { get; set; }

        public AddModel(IRestaurantServices restaurant)
        {
            this._restaurant = restaurant;
        }
        public IActionResult OnGet()
        {

            var check = HttpContext.Session.GetObject<Account>("Admin");
            if (check == null)
            {
                return Redirect("/Index");
            }
            Account = check;
            return Page();

        }
        public async Task<IActionResult> OnPostSubmit(RestaurantRequest request)
        {
            try
            {
                
                var restaurant = new RestaurantModel()
                {
                    RestaurantName = request.RestaurantName,
                    RestaurantAddress = request.RestaurantAddress,
                    CategoryName = request.CategoryName,
                    ZoneId = request.ZoneId
                };
                await _restaurant.Add(restaurant);
                Message = $"Succesfully added!";
                return Page();
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return Page();
            }


        }
    }
}
