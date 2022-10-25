using FoodDelivery.FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin
{
    public class RidersModel : PageModel
    {
        public Account? Account { get; set; }

        public IActionResult OnGet()
        {
            var check = HttpContext.Session.GetObject<Account>("Admin");
            if (check == null)
            {
                return Redirect("/Index");
            }
            Account = check;
            return Page();

        }
    }
}
