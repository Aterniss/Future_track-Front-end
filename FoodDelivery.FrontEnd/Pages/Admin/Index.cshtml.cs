using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly IAccountService _account;
        public Account? Account { get; set; }
        public string? Name { get; set; }
        public DateTime Date { get; set; }

        public IndexModel(IAccountService account)
        {
            this._account = account;
        }

        public IActionResult OnGet()
        {
            var check = HttpContext.Session.GetObject<Account>("Admin");
            if (check == null)
            {
                return Redirect("/Index");
            }
            Account = check;
            Date = DateTime.Now;
            return Page();

        }
        
    }
}
