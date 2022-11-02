using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Models.Requests;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.Dishes
{
    public class AddModel : PageModel
    {
        private readonly IDishService _dish;
        public Account? Account { get; set; }
        public DishRequest? Request { get; set; }
        public string Message { get; set; }

        public AddModel(IDishService dish)
        {
            this._dish = dish;
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
        public async Task<IActionResult> OnPostSubmit(DishRequest request, int restaurant)
        {
            try
            {
                if (request.Require18 == null)
                {
                    request.Require18 = false;
                }
                var dish = new Dish()
                {
                    DishName = request.DishName,
                    DishDescription = request.DishDescription,
                    Price = request.Price / 100,
                    Require18 = request.Require18,
                    RestaurantId = request.RestaurantId
        
                };
                await _dish.Add(dish);
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
