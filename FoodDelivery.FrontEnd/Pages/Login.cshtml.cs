using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAccountService _account;
        public Account Account { get; set; }
        public string Message { get; set; }


        public LoginModel(IAccountService account)
        {
            this._account = account;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostSubmit(Login login)
        {
            
            var result = await _account.Login(login.UserName, login.UserPassword);
            if (result == null)
            {
                Message = "Username or password is incorrect!";
                return Page();
            }
            Account = result;
            if (result.Role == 1)
            {
                return Redirect("/Customer");
            }
            else if(result.Role == 2)
            {
                return Redirect("/Privacy");
            }
            else if (result.Role == 3)
            {
                return Redirect("/Admin");
            }
            else
            {
                return Redirect("/Error");
            }

        }
    }
}
