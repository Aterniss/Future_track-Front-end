using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TFoodDelivery.FrontEnd.Models;
using TFoodDelivery.FrontEnd.Models.Requests;

namespace FoodDelivery.FrontEnd.Pages.Admin.Users
{
    public class AddModel : PageModel
    {
        private readonly IUserService _user;
        public Account? Account { get; set; }
        public UserRequest? Request { get; set; }
        public string Message { get; set; }

        public AddModel(IUserService user)
        {
            this._user = user;
        }
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
        public async Task<IActionResult> OnPostSubmit(UserRequest request)
        {
            try
            {
                if(request.IsOver18 == null)
                {
                    request.IsOver18 = false;
                }
                var user = new User()
                {
                    FullName = request.FullName,
                    CreatedAt = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    IsOver18 = request.IsOver18,
                    UserAddress = request.UserAddress
                };
                await _user.Add(user);
                return Redirect("/Admin/Users/Index");
            }
            catch(Exception ex)
            {
                Message = ex.Message;
                return Page();
            }
           
            
        }
    }
}
