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
            var result = await _zone.GetAll();
            Zones = result;
            Account = check;
            return Page();

        }
    }
}
