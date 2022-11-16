using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Models.Requests;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.Zones
{
    public class AddModel : PageModel
    {
        private readonly IZoneService _zone;
        public Account? Account { get; set; }
        public ZoneRequest? Request { get; set; }
        public string Message { get; set; }

        public AddModel(IZoneService zone)
        {
            this._zone = zone;
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
        public async Task<IActionResult> OnPostSubmit(ZoneRequest request)
        {
            try
            {
                var zone = new Zone()
                {
                    ZoneName = request.ZoneName
                };
                await _zone.Add(zone);
                return Redirect("/Admin/Zones/Index");
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return Page();
            }


        }
    }
}
