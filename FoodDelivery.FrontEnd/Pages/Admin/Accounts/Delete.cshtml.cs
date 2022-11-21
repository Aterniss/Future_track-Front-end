using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.Accounts
{
    public class DeleteModel : PageModel
    {
        private readonly IAccountService _account;
        public string Message { get; set; }
        public int Id { get; set; }

        public DeleteModel(IAccountService account)
        {
            this._account = account;   
        }
        public IActionResult OnGet(int id)
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
            return Redirect("/Admin/Accounts/Index");
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            try
            {
                await _account.Delete(id);
                return Redirect("/Admin/Accounts/Index");
            }
            catch (Exception e)
            {
                Message = e.Message;
                return Page();
            }

        }
    }
}
