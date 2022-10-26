using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TFoodDelivery.FrontEnd.Models;
using TFoodDelivery.FrontEnd.Models.Requests;

namespace FoodDelivery.FrontEnd.Pages.Admin.Users
{
    public class UpdateModel : PageModel
    {
        private readonly IUserService _user;
        public Account? Account { get; set; }
        public UserRequest? Request { get; set; }
        public User User { get; set; }
        public string Message { get; set; }
        public UpdateModel(IUserService user)
        {
            this._user = user;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {

            var check = HttpContext.Session.GetObject<Account>("Admin");
            
            if (check == null)
            {
                return Redirect("/Index");
            }
            Account = check;
            var user = await _user.GetById(id);
            User = user;
            return Page();

        }
    }
}
