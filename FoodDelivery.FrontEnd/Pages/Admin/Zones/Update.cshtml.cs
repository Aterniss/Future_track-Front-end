using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Models.Requests;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.Zones
{
    public class UpdateModel : PageModel
    {
        private readonly IZoneService _zone;
        public ZoneRequest? Request { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public string Message { get; set; }
        public UpdateModel(IZoneService zone)
        {
            this._zone = zone;
        }

        public IActionResult OnGetAsync(int id, string name)
        {
            var check = HttpContext.Session.GetObject<Account>("Admin");

            if (check == null)
            {
                return Redirect("/Index");
            }
            Name = name;
            Id = id;
            return Page();
        }

        public async Task<IActionResult> OnPostSubmit(ZoneRequest request, int id)
        {
            try
            {
                var zone = new Zone()
                {
                    ZoneName = request.ZoneName
                };
                await _zone.Update(zone, id);
                Message = $"Succesfully updated!";
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
