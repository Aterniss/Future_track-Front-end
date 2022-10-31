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
        private readonly IFoodCategoryService _foodCategoryService;
        private readonly IZoneService _zoneService;
        public IEnumerable<Zone> Zones { get; set; }
        public IEnumerable<FoodCategory> Categories { get; set; }
        public RestaurantRequest? Request { get; set; }
        public string Message { get; set; }

        public AddModel(IRestaurantServices restaurant, IFoodCategoryService foodCategoryService, IZoneService zoneService)
        {
            this._restaurant = restaurant;
            this._foodCategoryService = foodCategoryService;
            this._zoneService = zoneService;
        }
        public async Task<IActionResult> OnGet(string message)
        {
            if(message != null)
            {
                Message = message;
            }
            var check = HttpContext.Session.GetObject<Account>("Admin");
            if (check == null)
            {
                return Redirect("/Index");
            }
            try
            {
                Zones = await _zoneService.GetAll();
                Categories = await _foodCategoryService.GetAll();
                return Page();
            }
            catch (HttpRequestException)
            {
                Message = "You can not add new restaurant";
                return Page();
            }

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
