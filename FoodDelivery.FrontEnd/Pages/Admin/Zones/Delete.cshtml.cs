using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.Zones
{
    public class DeleteModel : PageModel
    {
        private readonly IZoneService _zone;
        public int Id { get; set; }

        public DeleteModel(IZoneService zone)
        {
            this._zone = zone;
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
            return Redirect("/Admin/Zones");
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            await _zone.Delete(id);
            return Redirect("/Admin/Zones");

        }
    }
}
