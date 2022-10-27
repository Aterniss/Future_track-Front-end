using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.Orders
{
    public class MoreInfoModel : PageModel
    {
        private readonly IOrderService _order;
        public Order OneOrder;
        public Account? Account { get; set; }

        public MoreInfoModel(IOrderService order)
        {
            this._order = order;
        }


        public async Task<IActionResult> OnGet(int id)
        {
            var check = HttpContext.Session.GetObject<Account>("Admin");
            if (check == null)
            {
                return Redirect("/Index");
            }
            var result = await _order.GetById(id);
            OneOrder = result;
            return Page();
        }
    }
}
