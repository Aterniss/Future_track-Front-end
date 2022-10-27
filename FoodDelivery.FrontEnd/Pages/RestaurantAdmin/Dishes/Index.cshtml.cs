using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.RestaurantAdmin.Dishes
{
    public class IndexModel : PageModel
    {
        private readonly IDishService _dish;
        public IEnumerable<Dish>? Dishes { get; set; }
        public int RestaurantId { get; set; }
        public Account Account { get; set; }


        public IndexModel(IDishService dish)
        {
            this._dish = dish;
        }


        public async Task<IActionResult> OnGetAsync()
        {

            var check = HttpContext.Session.GetObject<Account>("Restaurant");
            if (check == null)
            {
                return Redirect("/Index");
            }
            else
            {
                Account = check;
                if(Account.RestaurantId != null)
                {
                    int id = Account.RestaurantId.Value;
                    RestaurantId = id;
                    var result = await _dish.GetAllId(id);
                    Dishes = result;
                    return Page();
                }
                return Redirect("/Error");
                
                
            }
            
        }
    }
}
