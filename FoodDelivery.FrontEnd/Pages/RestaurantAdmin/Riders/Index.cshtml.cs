using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.RestaurantAdmin.Riders
{
    public class IndexModel : PageModel
    {
        private readonly IRiderService _rider;
        public IEnumerable<Rider>? Riders { get; set; }
        public int RestaurantId { get; set; }
        public Account Account { get; set; }

        public IndexModel(IRiderService rider)
        {
            this._rider = rider;
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
                if (Account.RestaurantId != null)
                {
                    int id = Account.RestaurantId.Value;
                    RestaurantId = id;
                    var result = await _rider.GetAllId(id);
                    Riders = result;
                    return Page();
                }
                return Redirect("/Error");


            }
        }
    }
}
