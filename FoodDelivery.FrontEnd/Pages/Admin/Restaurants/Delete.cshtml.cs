using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.Restaurants
{
    public class DeleteModel : PageModel
    {
        private readonly IRestaurantServices _restaurant;
        public int Id { get; set; }

        public DeleteModel(IRestaurantServices restaurant)
        {
            this._restaurant = restaurant;
        }

        public Account? Account { get; set; }

        public IActionResult OnGetAsync(int id)
        {

            var check = HttpContext.Session.GetObject<Account>("Admin");
            if (check == null)
            {
                return Redirect("/Index");
            }
            Id = id;
            return Page();
        }
        public IActionResult OnPostBack()
        {
            return Redirect("/Admin/Restaurants");
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            await _restaurant.Delete(id);
            return Redirect("/Admin/Restaurants");

        }
    }
}
