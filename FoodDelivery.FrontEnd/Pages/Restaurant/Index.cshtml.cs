using FoodDelivery.FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Restaurant
{
    public class IndexModel : PageModel
    {
        public Account? Account { get; set; }
        public IActionResult OnGet()
        {
            var check = HttpContext.Session.GetObject<Account>("Restaurant");
            if (check == null)
            {
                return Redirect("/Index");
            }
            Account = check;
            return Page();

        }
    }
}
