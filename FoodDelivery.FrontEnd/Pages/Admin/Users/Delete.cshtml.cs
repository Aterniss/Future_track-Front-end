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
        public string Message { get; set; }

        public DeleteModel(IUserService user)
        {
            this._user = user;
        }

        public Account? Account { get; set; }

        public IActionResult OnGetAsync(int id)
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
            return Redirect("/Admin/Users/Index");
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            try
            {
                await _user.Delete(id);
                return Redirect("/Admin/Users/Index");
            }
            catch (Exception e)
            {
                Message = e.Message;
                return Page();
            }
        }
        
    }
}
