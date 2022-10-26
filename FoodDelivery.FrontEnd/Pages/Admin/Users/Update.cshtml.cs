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
        public int Id { get; set; }
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
            Id = id;
            return Page();
        }
        public async Task<IActionResult> OnPostSubmit(UserRequest request, int id)
        {
            try
            {
                if (request.IsOver18 == null)
                {
                    request.IsOver18 = false;
                }
                var user = new User()
                {
                    FullName = request.FullName,
                    LastUpdate = DateTime.Now,
                    IsOver18 = request.IsOver18,
                    UserAddress = request.UserAddress
                };
                await _user.Update(user, id);
                Message = $"Succesfully updated!";
                return Redirect("/Admin/Users");
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return Page();
            }

        }
    }
}
