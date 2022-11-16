using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Models.Requests;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TFoodDelivery.FrontEnd.Models;

namespace FoodDelivery.FrontEnd.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountService _account;
        private readonly IUserService _user;

        public string Message { get; set; }


        public RegisterModel(IAccountService account, IUserService user)
        {
            this._account = account;
            this._user = user;
        }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostSubmit(AccountRequest request)
        {
            try
            {
                await _account.Register(request);

                return Redirect("/Login");
            }
            catch(Exception e)
            {
                Message = e.Message;
                return Page();
            }
        }

    }
}
