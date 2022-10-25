using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGetAsync()
        {
            HttpContext.Session.Remove("Admin");
            HttpContext.Session.Remove("Restaurant");
            HttpContext.Session.Remove("Customer");

            return Redirect("/Index");
        }
    }
}
