using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Models.Requests;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.Riders
{
    public class UpdateModel : PageModel
    {
        private readonly IRiderService _rider;
        public Account? Account { get; set; }
        public RiderRequest? Request { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Zone { get; set; }


        public string Message { get; set; }
        public UpdateModel(IRiderService rider)
        {
            this._rider = rider;
        }
        public IActionResult OnGetAsync(int id, string name, int zone)
        {
            var check = HttpContext.Session.GetObject<Account>("Admin");

            if (check == null)
            {
                return Redirect("/Index");
            }
            Account = check;
            Name = name;
            Zone = zone;
            Id = id;
            return Page();
        }
        public async Task<IActionResult> OnPostSubmit(RiderRequest request, int id)
        {
            try
            {
                var dish = new Rider()
                {
                    RiderName = request.RiderName,
                    ZoneId = request.ZoneId
                };
                await _rider.Update(dish, id);
                Message = $"Succesfully updated!";
                return Redirect("/Admin/Riders");
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return Page();
            }

        }
    }
}
