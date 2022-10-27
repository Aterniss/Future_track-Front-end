using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Models.Requests;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TFoodDelivery.FrontEnd.Models;

namespace FoodDelivery.FrontEnd.Pages.Admin.Restaurants
{
    public class UpdateModel : PageModel
    {
        private readonly IRestaurantServices _restaurant;
        public Account? Account { get; set; }
        public RestaurantRequest? Request { get; set; }
        public int Id { get; set; }
        public string RestaurantName { get; set; }
        public string CategoryName { get; set; }
        public string Address { get; set; }
        public int ZoneId { get; set; }

        public string Message { get; set; }
        public UpdateModel(IRestaurantServices restaurant)
        {
            this._restaurant = restaurant;
        }
        public IActionResult OnGetAsync(int id, string restName, string cat, string address, int zone)
        {

            var check = HttpContext.Session.GetObject<Account>("Admin");

            if (check == null)
            {
                return Redirect("/Index");
            }
            Account = check;
            Id = id;
            RestaurantName = restName;
            CategoryName = cat;
            Address = address;
            ZoneId = zone;
            return Page();
        }
        public async Task<IActionResult> OnPostSubmit(RestaurantRequest request, int id)
        {
            try
            {
                var restaurant = new RestaurantModel()
                {
                    RestaurantName = request.RestaurantName,
                    CategoryName = request.CategoryName,
                    RestaurantAddress = request.RestaurantAddress,
                    ZoneId = request.ZoneId
                };
                await _restaurant.Update(restaurant, id);
                return Redirect("/Admin/Restaurants");
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return Page();
            }

        }
    }
}
