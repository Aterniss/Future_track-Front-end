using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.RestaurantAdmin.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _order;
        public IEnumerable<Order>? Orders { get; set; }
        public int RestaurantId { get; set; }
        public Account Account { get; set; }

        public IndexModel(IOrderService order)
        {
            this._order = order;
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
                    var result = await _order.GetAllId(id);
                    Orders = result;
                    return Page();
                }
                return Redirect("/Error");


            }
        }
    }
}
