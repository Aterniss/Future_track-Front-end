using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _order;
        public IEnumerable<Order>? Orders { get; set; }
        public Order? OneOrder { get; set; }
        public Account? Account { get; set; }


        public IndexModel(IOrderService order)
        {
            this._order = order;
        }


        public async Task<IActionResult> OnGetAsync()
        {

            var check = HttpContext.Session.GetObject<Account>("Admin");
            if (check == null)
            {
                return Redirect("/Index");
            }
            var result = await _order.GetAll();
            Orders = result;
            Account = check;
            return Page();

        }
    }
}
