using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.Accounts
{
    public class MoreinfoModel : PageModel
    {
        private readonly IAccountService _account;
        public Account OneAccount;
        public string Message { get; set; }

        public MoreinfoModel(IAccountService account)
        {
            this._account = account;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            var check = HttpContext.Session.GetObject<Account>("Admin");
            if (check == null)
            {
                return Redirect("/Index");
            }
            try
            {
                var result = await _account.GetById(id);
                OneAccount = result;
                return Page();
            }
            catch (Exception e)
            {
                Message = e.Message;
                return Page();
            }
        }
    }
}
