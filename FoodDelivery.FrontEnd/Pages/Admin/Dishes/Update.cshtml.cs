using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Models.Requests;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.Dishes
{
    public class UpdateModel : PageModel
    {
        private readonly IDishService _dish;
        public Account? Account { get; set; }
        public DishRequest? Request { get; set; }
        public int Id { get; set; }
        public string DishName { get; set; }
        public string DishDescription { get; set; }
        public int RestaurantId { get; set; }
        public Decimal Price { get; set; }
        public bool Require18 { get; set; }


        public string Message { get; set; }
        public UpdateModel(IDishService dish)
        {
            this._dish = dish;
        }
        public IActionResult OnGetAsync(int id, string name, string desc, int restaurant, decimal price, bool require)
        {
            var check = HttpContext.Session.GetObject<Account>("Admin");

            if (check == null)
            {
                return Redirect("/Index");
            }
            Account = check;
            DishName = name;
            DishDescription = desc;
            RestaurantId = restaurant;
            Price = price;
            Require18 = require;

            Id = id;
            return Page();
        }
        public async Task<IActionResult> OnPostSubmit(DishRequest request, int id)
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
                    Price = request.Price,
                    RestaurantId = request.RestaurantId,
                    Require18 = request.Require18
                };
                await _dish.Update(dish, id);
                Message = $"Succesfully updated!";
                return Redirect("/Admin/FoodCategories");
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return Page();
            }

        }
    }
}
