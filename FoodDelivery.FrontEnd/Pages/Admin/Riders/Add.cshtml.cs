using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Models.Requests;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.Riders
{
    public class AddModel : PageModel
    {
        private readonly IRiderService _rider;
        public Account? Account { get; set; }
        public string Message { get; set; }

        public AddModel(IRiderService rider)
        {
            this._rider = rider;
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
        public async Task<IActionResult> OnPostSubmit(RiderRequest request)
        {
            try
            {
                var dish = new Rider()
                {
                    RiderName = request.RiderName,
                    ZoneId = request.ZoneId
                };
                await _rider.Add(dish);
                Message = $"Succesfully added!";
                return Page();
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return Page();
            }


        }
    }
}
