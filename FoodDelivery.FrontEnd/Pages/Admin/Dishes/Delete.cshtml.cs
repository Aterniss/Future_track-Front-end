using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.Dishes
{
    public class DeleteModel : PageModel
    {
        private readonly IDishService _dish;
        public int Id { get; set; }

        public DeleteModel(IDishService dish)
        {
            this._dish = dish;
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
            return Redirect("/Admin/Dishes");
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            await _dish.Delete(id);
            return Redirect("/Admin/Dishes");

        }
    }
}
