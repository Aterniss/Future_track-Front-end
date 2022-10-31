using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.Zones
{
    public class IndexModel : PageModel
    {
        private readonly IZoneService _zone;
        public IEnumerable<Zone>? Zones { get; set; }
        public Zone? OneZone { get; set; }
        public Account? Account { get; set; }
        public string Message { get; set; }


        public IndexModel(IZoneService zone)
        {
            this._zone = zone;
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
                var result = await _zone.GetAll();
                Zones = result;
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
