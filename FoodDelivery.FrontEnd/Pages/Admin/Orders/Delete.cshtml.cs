using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.Orders
{
    public class DeleteModel : PageModel
    {
        private readonly IOrderService _order;
        public int Id { get; set; }

        public DeleteModel(IOrderService order)
        {
            this._order = order;
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
            return Redirect("/Admin/Orders");
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            await _order.Delete(id);
            return Redirect("/Admin/Orders");

        }
    }
}
