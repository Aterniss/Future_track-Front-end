using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.Riders
{
    public class IndexModel : PageModel
    {
        private readonly IRiderService _rider;
        public IEnumerable<Rider>? Riders { get; set; }
        public Rider? OneRider { get; set; }
        public Account? Account { get; set; }
        public string Message { get; set; }


        public IndexModel(IRiderService rider)
        {
            this._rider = rider;
        }


        public async Task<IActionResult> OnGetAsync()
        {

            var check = HttpContext.Session.GetObject<Account>("Admin");
            if (check == null)
            {
                return Redirect("/Index");
            }
            try
            {
                var result = await _rider.GetAll();
                Riders = result;
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
