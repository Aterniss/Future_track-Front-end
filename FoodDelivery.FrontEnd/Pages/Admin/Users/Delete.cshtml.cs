using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.Users
{
    public class DeleteModel : PageModel
    {
        private readonly IUserService _user;
        public int Id { get; set; }

        public DeleteModel(IUserService user)
        {
            this._user = user;
        }

        public Account? Account { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {

            var check = HttpContext.Session.GetObject<Account>("Admin");
            if (check == null)
            {
                return Redirect("/Index");
            }
            Id = id;
            var result = await _user.GetAll();
            return Page();
        }
        public IActionResult OnPostBack()
        {
            return Redirect("/Admin/Users");
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            await _user.Delete(id);
            return Redirect("/Admin/Users");

        }
        
    }
}
