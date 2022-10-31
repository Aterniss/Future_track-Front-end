using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TFoodDelivery.FrontEnd.Models;

namespace FoodDelivery.FrontEnd.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _user;
        public IEnumerable<User>? Users { get; set; }
        public User? OneUser { get; set; }
        public string Message { get; set; }

        public IndexModel(IUserService user)
        {
            this._user = user;
        }

        public Account? Account { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            var check = HttpContext.Session.GetObject<Account>("Admin");
            if (check == null)
            {
                return Redirect("/Index");
            }
            try
            {
                var result = await _user.GetAll();
                Users = result;
                Account = check;
                return Page();
            }
            catch(HttpRequestException e)
            {
                Message = e.Message;
                return Page();
            }
            

        }
    }
}
