using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.RestaurantAdmin.Dishes
{
    public class DeleteModel : PageModel
    {
            private readonly IDishService _dish;
            public int Id { get; set; }
            public string Message { get; set; }
            public DeleteModel(IDishService dish)
            {
                this._dish = dish;
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
                return Redirect("/RestaurantAdmin/Dishes/Index");
            }
            public async Task<IActionResult> OnPostDelete(int id)
            {
                try
                {
                    await _dish.Delete(id);
                    return Redirect("/RestaurantAdmin/Dishes/Index");
                }
                catch (Exception e)
                {
                    Message = e.Message;
                    return Page();
                }

            }
        }
}
