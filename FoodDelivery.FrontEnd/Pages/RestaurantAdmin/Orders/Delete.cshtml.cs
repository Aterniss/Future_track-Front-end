using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.RestaurantAdmin.Orders
{
    public class DeleteModel : PageModel
    {
        private readonly IOrderService _order;
        public int Id { get; set; }
        public string Message { get; set; }
        public DeleteModel(IOrderService order)
        {
            this._order = order;
        }

        public IActionResult OnGetAsync(int id)
        {

            var check = HttpContext.Session.GetObject<Account>("Restaurant");
            if (check == null)
            {
                return Redirect("/Index");
            }
            Id = id;
            return Page();
        }
        public IActionResult OnPostBack()
        {
            return Redirect("/RestaurantAdmin/Orders/Index");
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            try
            {
                await _order.Delete(id);
                return Redirect("/RestaurantAdmin/Orders/Index");
            }
            catch (Exception e)
            {
                Message = e.Message;
                return Page();
            }

        }
    }
}
