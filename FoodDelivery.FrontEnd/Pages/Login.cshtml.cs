using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAccountService _account;

        public Account? Account { get; set; }
        public string? Message { get; set; }
        public string Exception { get; set; }


        public LoginModel(IAccountService account)
        {
            this._account = account;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostSubmit(Login login)
        {
            try
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
                    HttpContext.Session.SetObject("Customer", result);
                    return Redirect("/Customer");
                }
                else if (result.Role == 2)
                {
                    HttpContext.Session.SetObject("Restaurant", result);
                    return Redirect("/RestaurantAdmin");
                }
                else if (result.Role == 3)
                {
                    HttpContext.Session.SetObject("Admin", result);
                    return Redirect("/Admin");
                }
                else
                {
                    return Redirect("/Error");
                }
            }
            catch(HttpRequestException e)
            {
                Exception = e.Message;
                return Page();
            }
            

        }
    }
}
