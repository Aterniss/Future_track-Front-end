using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.Riders
{
    public class DeleteModel : PageModel
    {
        private readonly IRiderService _rider;
        public int Id { get; set; }
        public string Message { get; set; }

        public DeleteModel(IRiderService rider)
        {
            this._rider = rider;
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
            return Redirect("/Admin/Riders");
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            try
            {
                await _rider.Delete(id);
                return Redirect("/Admin/Riders");
            }
            catch(Exception e)
            {
                Message = e.Message;
                return Page();
            }

        }
    }
}
